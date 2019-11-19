using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lockstep.UnsafeECS {
    public static class UnsafeUtility {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe void* Malloc(long size, int alignment, Allocator allocator){
            var remain = size % alignment;
            if (remain != 0) {
                size = size + (alignment - remain);
            }
            return NativeUtil.Alloc((int) size).ToPointer();
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe void Free(void* memory, Allocator allocator){
            NativeUtil.Free(new IntPtr(memory));
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe void MemCpy(void* destination, void* source, long size){
            NativeUtil.Copy(destination, source, (int) size);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe void MemMove(void* destination, void* source, long size){
            NativeUtil.Copy(destination, source, (int) size);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe void MemClear(void* destination, long size){
            NativeUtil.Zero((byte*) destination, (int) size);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static unsafe bool MemCmp(void* ptr1, void* ptr2, long size){
            if (ptr1 == ptr2) return true;
            return NativeUtil.Compare((byte*) ptr1, (byte*) ptr2, (int) size);
        }


        public static unsafe void CopyPtrToStructure<T>(void* ptr, out T output) where T : unmanaged{
            output = *(T*) ptr;
        }

        public static unsafe void CopyStructureToPtr<T>(ref T input, void* ptr) where T : unmanaged{
            *(T*) ptr = input;
        }

        public static unsafe T ReadArrayElement<T>(void* source, int index) where T : unmanaged{
            return ((T*) source)[index];
        }

        public static unsafe T ReadArrayElementWithStride<T>(void* source, int index, int stride) where T : unmanaged{
            return *(T*) ((IntPtr) source + index * stride);
        }

        public static unsafe void WriteArrayElement<T>(void* destination, int index, T value) where T : unmanaged{
            ((T*) destination)[index] = value;
        }

        public static unsafe void WriteArrayElementWithStride<T>(
            void* destination,
            int index,
            int stride,
            T value) where T : unmanaged{
            *(T*) ((IntPtr) destination + index * stride) = value;
        }

        public static unsafe void* AddressOf<T>(ref T output) where T : unmanaged{
            throw new Exception("TODO AddressOf check the code is right!!");
            var ss = __makeref(output);
            TypedReference tr = __makeref(output);
            return (void*)(**(IntPtr**)(&tr));
            //fixed (byte** ptr = &output) {
            //    return (void*) *ptr;
            //}
        }

        public static unsafe int SizeOf<T>() where T : unmanaged{
            return sizeof(T);
        }

        public static int AlignOf<T>() where T : struct{
            return 4;
        }

        public static bool IsValidAllocator(Allocator allocator){
            return allocator > Allocator.None;
        }


#if false
#endif
    }
}