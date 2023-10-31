using Zenject;

namespace _GAME.Code.Features
{
    public class CharacterFeature
    {
        [Inject] private SaveFeature _saveFeature;
        
        public bool IsCharacterKilled(string id)
        {
            if (_saveFeature.SaveData.KilledEnemiesIds.Contains(id))
                return true;

            return false;
        }
        
        public void AddToKilledEnemies(string id)
        {
            _saveFeature.SaveData.KilledEnemiesIds.Add(id);
        }
        
    }
}