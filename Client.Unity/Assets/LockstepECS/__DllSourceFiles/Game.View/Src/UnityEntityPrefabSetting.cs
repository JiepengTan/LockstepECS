using System.Collections.Generic;
using Lockstep.Game;
using Unity.Entities;
using UnityEngine;

namespace SSSamples.Boids.Authoring {
    [RequiresEntityConversion]
    public class UnityEntityPrefabSetting : MonoBehaviour,IDeclareReferencedPrefabs, IConvertGameObjectToEntity {
        public List<UnityPrefabInfo> prefabInfos = new List<UnityPrefabInfo>();
        private List<EntityPrefabInfo> entityPrefabs = new List<EntityPrefabInfo>();
            
        // Lets you convert the editor data representation to the entity optimal runtime representation
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            foreach (var info in prefabInfos) {
                var entityPrefab = conversionSystem.GetPrimaryEntity(info.Prefab);
                entityPrefabs.Add(new EntityPrefabInfo() {
                        Prefab =  entityPrefab,
                        AssetId = info.AssetId
                    })
                ;
            }
            
            Lockstep.Game.UnityEntityService.RegisterUnityEntityPrefabs(entityPrefabs);
        }

        // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        { 
            foreach (var prefab in prefabInfos) {
                referencedPrefabs.Add(prefab.Prefab);
            }
        }
    }
}