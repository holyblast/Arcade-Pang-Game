
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace PangGame
{
    /// <summary>
    /// The main logic of the game,
    /// it holds the data of each level read from a json file at runtime.
    /// </summary>
    public class GameModel
    {
        private DataModel _dataModel;

        private const string PathToData = "Data";
        private const string CurrentArea = "CurrentArea";
        private const string CurrentInnerArea = "CurrentInnerArea";
        private const string AreaProgress = "AreaProgress";

        public int Area
        {
            get => PlayerPrefs.GetInt(CurrentArea, 0);
            set
            {
                if (value < _dataModel.levelData.Count)
                    PlayerPrefs.SetInt(CurrentArea, value);
            }
        }

        public int InnerArea
        {
            get => PlayerPrefs.GetInt(CurrentInnerArea, 0);
            set => PlayerPrefs.SetInt(CurrentInnerArea, value);
        }

        public int HighestStageReached
        {
            get => PlayerPrefs.GetInt(AreaProgress, 1);
            set
            {
                if (value < (_dataModel.levelData.Count + 1))
                    PlayerPrefs.SetInt(AreaProgress, value);
            }
        }

        public GameResultType gameResult { get; set; }

        public GameModel()
        {
            _dataModel = new DataModel();

            LoadJsonData();
        }

        private void LoadJsonData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(PathToData);

            if (jsonFile != null)
            {
                string jsonContents = jsonFile.text;

                _dataModel = JsonConvert.DeserializeObject<DataModel>(jsonContents);
                
            }
            else
            {
                Debug.LogError($"Failed to load JSON file: {PathToData}");
            }
        }


        public Vector2 GetVelocityFromData(BubbleSizeType bubbleSizeType)
        {
            return _dataModel.enemyVelocityData[(int)bubbleSizeType];
        }

        public float GetSizeFromData(BubbleSizeType bubbleSizeType)
        {
            return _dataModel.enemySizeData[(int)bubbleSizeType];
        }

        public int GetMaxWeaponUsageOfType(WeaponType weaponType)
        {
            return _dataModel.maxWeaponUsageInEffect[(int)weaponType];
        }

        public AreaDataModel GetAreaData()
        {
            return _dataModel.levelData[Area];
        }

        public AreaInnerDataModel GetAreaInnerData()
        {
            return _dataModel.levelData[Area].areaStages[InnerArea];
        }

        public List<EnemyDataModel> GetEnemyData()
        {
            return _dataModel.levelData[Area].areaStages[InnerArea].enemyData;
        }

        public bool HasMoreInnerAreas()
        {
            return InnerArea + 1 < _dataModel.levelData[Area].areaStages.Count;
        }

        public void CheckWinCondition()
        {
            if (HasMoreInnerAreas())
            {
                gameResult = GameResultType.WinAreaInner;
            }
            else
            {
                gameResult = GameResultType.WinArea;
            }
        }

        public void SetAreaData()
        {
            switch (gameResult)
            {
                case GameResultType.WinAreaInner:
                    InnerArea++;
                    break;
                case GameResultType.WinArea:
                    Area++;
                    InnerArea = 0;
                    HighestStageReached++;
                    break;
            }
        }

        public GameResultType GetGameResult()
        {
            return gameResult;
        }

        public Vector2 GetPlayerPosition()
        {
            return GetAreaInnerData().playerStartingPosition;
        }
    }
}
