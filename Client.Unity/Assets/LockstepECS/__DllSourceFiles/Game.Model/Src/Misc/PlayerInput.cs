
using Lockstep.Math;
using Lockstep.Serialization;

namespace Lockstep.Game {
    public partial class PlayerInput : BaseFormater,IBaseComponent {
        public static PlayerInput Empty = new PlayerInput();
        public ushort Deg;
        public ushort SkillId;

        public override bool Equals(object obj){
            if (ReferenceEquals(this,obj)) return true;
            var other = obj as PlayerInput;
            return Equals(other);
        }
        
        public override void Serialize(Serializer writer){
            writer.Write(Deg);
            writer.Write(SkillId);
        }

        public void Reset(){
            Deg = 0;
            SkillId = 0;
        }

        public override void Deserialize(Deserializer reader){
            Deg = reader.ReadUInt16();
            SkillId = reader.ReadUInt16();
        }


        public bool Equals(PlayerInput other){
            if (other == null) return false;
            if (Deg != other.Deg) return false;
            if (SkillId != other.SkillId) return false;
            return true;
        }

        public PlayerInput Clone(){
            var tThis = this;
            return new PlayerInput() {
                SkillId = tThis.SkillId,
                Deg = tThis.Deg,
            };
        }
    }
}