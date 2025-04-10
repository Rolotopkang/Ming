    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autohand;
    using Cysharp.Threading.Tasks;
    using Tools;
    using UnityEngine;

    public class RoguelikeManager : Singleton<RoguelikeManager>
    {
        public int layer = 0;
        public int BigLayer = 1;
        public GameObject playerroot;

        public AnimationCurve ItemCurve;
        public AnimationCurve MoneyCurve;
        public AnimationCurve HealthCurve;
        public AnimationCurve EventCurve;
        public AnimationCurve StoreCurve;
        public AnimationCurve BossCurve;
        
        
        public List<RoomBase> RoomBaseList;

        public Transform zanshicunfang;
        private RoomBase currentRoom;
        private Dictionary<EnumTools.RoomKind, AnimationCurve> roomCurves;
        protected override void Awake()
        {
            base.Awake();
            roomCurves = new Dictionary<EnumTools.RoomKind, AnimationCurve>
            {
                { EnumTools.RoomKind.Item, ItemCurve },
                { EnumTools.RoomKind.Money, MoneyCurve },
                { EnumTools.RoomKind.Health, HealthCurve },
                { EnumTools.RoomKind.Event, EventCurve },
                { EnumTools.RoomKind.Store, StoreCurve },
                { EnumTools.RoomKind.Boss, BossCurve }
            };
        }

        private void OnEnable()
        {
            EventCenter.Subscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
            EventCenter.Subscribe(EnumTools.GameEvent.LevelEnd,OnLevelEnd);
        }

        private void OnDisable()
        {
            EventCenter.Unsubscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
            EventCenter.Unsubscribe(EnumTools.GameEvent.LevelEnd,OnLevelEnd);
        }

        private void OnLevelStart(Dictionary<String, object> args)
        {
            layer++;
            TeleportDoorsController.GetInstance().HideDoors(zanshicunfang);
            ItemUpgradeTabel.GetInstance().updateNewPosition(zanshicunfang);
            ItemTable.GetInstance().updateNewPosition(zanshicunfang);
            ItemForgingTable.GetInstance().updateNewPosition(zanshicunfang);
            RewardPickTable.GetInstance().updateNewPosition(zanshicunfang);
        }

        private void OnLevelEnd(Dictionary<String,object> args)
        {
            TeleportDoorsController.GetInstance().SetNextLevelDoor(GetTwoRandomRooms().ToList(),currentRoom.currentDoorPos.ToList());
        }

        public void Test()
        {
            NextLevel(EnumTools.RoomKind.Item);
        }

        public async void NextLevel(EnumTools.RoomKind roomKind)
        {
            await UniTask.WaitForSeconds(0.1f);
            SetCurrentRoom(GetRoomBaseByType(roomKind));
            EventCenter.Publish(EnumTools.GameEvent.LevelStart, new Dictionary<string, object>
            {
                { "CurrentLayer", layer},
                { "RoomKind", roomKind }
            });
        }

        private void SetCurrentRoom(RoomBase roomBase)
        {
            foreach (RoomBase room in RoomBaseList)
            {
                room.gameObject.SetActive(room.Equals(roomBase));
            }
            
            currentRoom = roomBase;
            Debug.Log(roomBase.currentBirthPoint.position+" --------------" + roomBase.name.ToString());
            playerroot.GetComponent<AutoHandPlayer>().SetPosition(roomBase.currentBirthPoint.position);
        }

        private RoomBase GetRoomBaseByType(EnumTools.RoomKind kind)
        {
            foreach (RoomBase roomBase in RoomBaseList)
            {
                if (roomBase.RoomKind == kind)
                {
                    return roomBase;
                }
            }

            Debug.LogError("没找到对应地图");
            return null;
        }
        
        public EnumTools.RoomKind[] GetTwoRandomRooms()
        {
            // 计算每个房间的权重
            var roomWeights = roomCurves.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Evaluate(layer)
            );

            // 移除权重为0的房间
            var validRooms = roomWeights.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);

            if (validRooms.Count < 2)
            {
                Debug.LogError("可选房间不足2个，检查权重曲线！");
                return new EnumTools.RoomKind[] { EnumTools.RoomKind.Item, EnumTools.RoomKind.Money }; // 兜底逻辑
            }

            // 随机抽取两个不同的房间
            EnumTools.RoomKind room1 = GetRandomRoom(validRooms);
            validRooms.Remove(room1);
            EnumTools.RoomKind room2 = GetRandomRoom(validRooms);

            return new EnumTools.RoomKind[] { room1, room2 };
        }
        
        private EnumTools.RoomKind GetRandomRoom(Dictionary<EnumTools.RoomKind, float> roomWeights)
        {
            float totalWeight = roomWeights.Values.Sum();
            float randomValue = UnityEngine.Random.Range(0f, totalWeight);
            float currentWeight = 0f;

            foreach (var room in roomWeights)
            {
                currentWeight += room.Value;
                if (randomValue <= currentWeight)
                    return room.Key;
            }

            return roomWeights.Keys.First(); // 兜底返回第一个房间
        }
    }
