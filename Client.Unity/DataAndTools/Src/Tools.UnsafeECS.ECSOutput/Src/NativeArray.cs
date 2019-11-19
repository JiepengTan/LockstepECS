
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lockstep.UnsafeECS {
  public enum Allocator :int
  {
    /// <summary>
    ///   <para>Invalid allocation.</para>
    /// </summary>
    Invalid,
    /// <summary>
    ///   <para>No allocation.</para>
    /// </summary>
    None,
    /// <summary>
    ///   <para>Temporary allocation.</para>
    /// </summary>
    Temp,
    /// <summary>
    ///   <para>Temporary job allocation.</para>
    /// </summary>
    TempJob,
    /// <summary>
    ///   <para>Persistent allocation.</para>
    /// </summary>
    Persistent,
  }
  
  public enum NativeArrayOptions :int
  {
    /// <summary>
    ///         <para>Uninitialized memory can improve performance, but results in the contents of the array elements being undefined.
    /// In performance sensitive code it can make sense to use NativeArrayOptions.Uninitialized, if you are writing to the entire array right after creating it without reading any of the elements first.
    /// </para>
    ///       </summary>
    UninitializedMemory,
    /// <summary>
    ///   <para>Clear NativeArray memory on allocation.</para>
    /// </summary>
    ClearMemory,
  }

  [StructLayout(LayoutKind.Explicit, Pack = 4)]
  public unsafe struct NativeArrayss<T>  {
    [FieldOffset(0)]
    internal int m_ElemSize;
  }

  /// <summary>
  ///   <para>A NativeArray exposes a buffer of native memory to managed code, making it possible to share data between managed and native without marshalling costs.</para>
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public unsafe struct NativeArray<T> : IDisposable, IEnumerable<T>, IEquatable<NativeArray<T>>, IEnumerable
    where T : unmanaged {
    internal int m_ElemSize;
    internal void* m_Buffer;
    internal int m_Length;
    internal int m_MinIndex;
    internal int m_MaxIndex;
    internal Allocator m_AllocatorLabel;

    public unsafe NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
    {
      NativeArray<T>.Allocate(length, allocator, out this);
      if ((options & NativeArrayOptions.ClearMemory) != NativeArrayOptions.ClearMemory)
        return;
      UnsafeUtility.MemClear(this.m_Buffer, (long) this.Length * (long) UnsafeUtility.SizeOf<T>());
    }

    public NativeArray(T[] array, Allocator allocator)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      NativeArray<T>.Allocate(array.Length, allocator, out this);
      NativeArray<T>.Copy(array, this);
    }

    public NativeArray(NativeArray<T> array, Allocator allocator)
    {
      NativeArray<T>.Allocate(array.Length, allocator, out this);
      NativeArray<T>.Copy(array, this);
    }

    private static unsafe void Allocate(int length, Allocator allocator, out NativeArray<T> array)
    {
      long size = (long) UnsafeUtility.SizeOf<T>() * (long) length;
      if (allocator <= Allocator.None)
        throw new ArgumentException("Allocator must be Temp, TempJob or Persistent", nameof (allocator));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), "Length must be >= 0");
      NativeArray<T>.IsBlittableAndThrow();
      if (size > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (length), string.Format("Length * sizeof(T) cannot exceed {0} bytes", (object) int.MaxValue));
      array.m_Buffer = UnsafeUtility.Malloc(size, UnsafeUtility.AlignOf<T>(), allocator);
      array.m_Length = length;
      array.m_AllocatorLabel = allocator;
      array.m_MinIndex = 0;
      array.m_MaxIndex = length - 1;
      array.m_ElemSize = sizeof(T);
    }

    public int Length
    {
      get
      {
        return this.m_Length;
      }
    }

    internal static void IsBlittableAndThrow()
    {
      //TODO check it
      //if (!UnsafeUtility.IsBlittable<T>())
      //  throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be blittable.\n{2}", (object) typeof (T), (object) typeof (T), (object) UnsafeUtility.GetReasonForValueTypeNonBlittable<T>()));
    }

    [Conditional("DEBUG")]
    private unsafe void CheckElementReadAccess(int index)
    {
      if (index < this.m_MinIndex || index > this.m_MaxIndex)
        this.FailOutOfRangeError(index);
      
    }

    [Conditional("DEBUG")]
    private unsafe void CheckElementWriteAccess(int index)
    {
      if (index < this.m_MinIndex || index > this.m_MaxIndex)
        this.FailOutOfRangeError(index);
     
    }

    public unsafe T* GetPointer(int index){
      this.CheckElementReadAccess(index);
      return (T*)((byte*)m_Buffer + m_ElemSize * index);
    }
    
    public unsafe T this[int index]
    {
      get
      {
        this.CheckElementReadAccess(index);
        return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
      }
      set
      {
        this.CheckElementWriteAccess(index);
        UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
      }
    }

    public unsafe bool IsCreated
    {
      get
      {
        return (IntPtr) this.m_Buffer != IntPtr.Zero;
      }
    }

    public unsafe void Dispose()
    {
      if (!UnsafeUtility.IsValidAllocator(this.m_AllocatorLabel))
        throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
      UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
      this.m_Buffer = (void*) null;
      this.m_Length = 0;
    }

    public void CopyFrom(T[] array)
    {
      NativeArray<T>.Copy(array, this);
    }

    public void CopyFrom(NativeArray<T> array)
    {
      NativeArray<T>.Copy(array, this);
    }

    public void CopyTo(T[] array)
    {
      NativeArray<T>.Copy(this, array);
    }

    public void CopyTo(NativeArray<T> array)
    {
      NativeArray<T>.Copy(this, array);
    }

    public T[] ToArray()
    {
      T[] dst = new T[this.Length];
      NativeArray<T>.Copy(this, dst, this.Length);
      return dst;
    }

    private void FailOutOfRangeError(int index)
    {
      if (index < this.Length && (this.m_MinIndex != 0 || this.m_MaxIndex != this.Length - 1))
        throw new IndexOutOfRangeException(string.Format("Index {0} is out of restricted IJobParallelFor range [{1}...{2}] in ReadWriteBuffer.\n", (object) index, (object) this.m_MinIndex, (object) this.m_MaxIndex) + "ReadWriteBuffers are restricted to only read & write the element at the job index. You can use double buffering strategies to avoid race conditions due to reading & writing in parallel to the same elements from a job.");
      throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of '{1}' Length.", (object) index, (object) this.Length));
    }

    public NativeArray<T>.Enumerator GetEnumerator()
    {
      return new NativeArray<T>.Enumerator(ref this);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new NativeArray<T>.Enumerator(ref this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public unsafe bool Equals(NativeArray<T> other)
    {
      return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      return obj is NativeArray<T> && this.Equals((NativeArray<T>) obj);
    }

    public override unsafe int GetHashCode()
    {
      return (int) this.m_Buffer * 397 ^ this.m_Length;
    }

    public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
    {
      return !left.Equals(right);
    }

    public static void Copy(NativeArray<T> src, NativeArray<T> dst)
    {
      if (src.Length != dst.Length)
        throw new ArgumentException("source and destination length must be the same");
      NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
    }

    public static void Copy(T[] src, NativeArray<T> dst)
    {
      if (src.Length != dst.Length)
        throw new ArgumentException("source and destination length must be the same");
      NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
    }

    public static void Copy(NativeArray<T> src, T[] dst)
    {
      if (src.Length != dst.Length)
        throw new ArgumentException("source and destination length must be the same");
      NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
    }

    public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
    {
      NativeArray<T>.Copy(src, 0, dst, 0, length);
    }

    public static void Copy(T[] src, NativeArray<T> dst, int length)
    {
      NativeArray<T>.Copy(src, 0, dst, 0, length);
    }

    public static void Copy(NativeArray<T> src, T[] dst, int length)
    {
      NativeArray<T>.Copy(src, 0, dst, 0, length);
    }

    public static unsafe void Copy(
      NativeArray<T> src,
      int srcIndex,
      NativeArray<T> dst,
      int dstIndex,
      int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), "length must be equal or greater than zero.");
      if (srcIndex < 0 || srcIndex > src.Length || srcIndex == src.Length && src.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (srcIndex), "srcIndex is outside the range of valid indexes for the source NativeArray.");
      if (dstIndex < 0 || dstIndex > dst.Length || dstIndex == dst.Length && dst.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (dstIndex), "dstIndex is outside the range of valid indexes for the destination NativeArray.");
      if (srcIndex + length > src.Length)
        throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source NativeArray.", nameof (length));
      if (dstIndex + length > dst.Length)
        throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination NativeArray.", nameof (length));
      UnsafeUtility.MemCpy(
        (void*) ((byte*) dst.m_Buffer + (dstIndex * UnsafeUtility.SizeOf<T>())), 
        (void*) ((byte*) src.m_Buffer +  (srcIndex * UnsafeUtility.SizeOf<T>())), 
        (long) (length * UnsafeUtility.SizeOf<T>()));
    }

    public static unsafe void Copy(
      T[] src,
      int srcIndex,
      NativeArray<T> dst,
      int dstIndex,
      int length)
    {
      if (src == null)
        throw new ArgumentNullException(nameof (src));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), "length must be equal or greater than zero.");
      if (srcIndex < 0 || srcIndex > src.Length || srcIndex == src.Length && src.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (srcIndex), "srcIndex is outside the range of valid indexes for the source array.");
      if (dstIndex < 0 || dstIndex > dst.Length || dstIndex == dst.Length && dst.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (dstIndex), "dstIndex is outside the range of valid indexes for the destination NativeArray.");
      if (srcIndex + length > src.Length)
        throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source array.", nameof (length));
      if (dstIndex + length > dst.Length)
        throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination NativeArray.", nameof (length));
      GCHandle gcHandle = GCHandle.Alloc((object) src, GCHandleType.Pinned);
      IntPtr num = gcHandle.AddrOfPinnedObject();
      UnsafeUtility.MemCpy(
        (void*) ((byte*) dst.m_Buffer +  (dstIndex * UnsafeUtility.SizeOf<T>())),
        (void*) ((byte*) (void*) num +  (srcIndex * UnsafeUtility.SizeOf<T>())), 
        (long) (length * UnsafeUtility.SizeOf<T>()));
      gcHandle.Free();
    }

    public static unsafe void Copy(
      NativeArray<T> src,
      int srcIndex,
      T[] dst,
      int dstIndex,
      int length)
    {
      if (dst == null)
        throw new ArgumentNullException(nameof (dst));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), "length must be equal or greater than zero.");
      if (srcIndex < 0 || srcIndex > src.Length || srcIndex == src.Length && src.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (srcIndex), "srcIndex is outside the range of valid indexes for the source NativeArray.");
      if (dstIndex < 0 || dstIndex > dst.Length || dstIndex == dst.Length && dst.Length > 0)
        throw new ArgumentOutOfRangeException(nameof (dstIndex), "dstIndex is outside the range of valid indexes for the destination array.");
      if (srcIndex + length > src.Length)
        throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source NativeArray.", nameof (length));
      if (dstIndex + length > dst.Length)
        throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination array.", nameof (length));
      GCHandle gcHandle = GCHandle.Alloc((object) dst, GCHandleType.Pinned);
      UnsafeUtility.MemCpy(
        (void*) ((byte*)(void*)gcHandle .AddrOfPinnedObject() +  (dstIndex * UnsafeUtility.SizeOf<T>())), 
        (void*) ((byte*) src.m_Buffer +  (srcIndex * UnsafeUtility.SizeOf<T>())), 
        (long) (length * UnsafeUtility.SizeOf<T>()));
      gcHandle.Free();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
    {
      private NativeArray<T> m_Array;
      private int m_Index;

      public Enumerator(ref NativeArray<T> array)
      {
        this.m_Array = array;
        this.m_Index = -1;
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        ++this.m_Index;
        return this.m_Index < this.m_Array.Length;
      }

      public void Reset()
      {
        this.m_Index = -1;
      }

      public T Current
      {
        get
        {
          return this.m_Array[this.m_Index];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }
    }
  }
}
