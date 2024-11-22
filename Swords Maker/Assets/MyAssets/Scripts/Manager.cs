using UnityEngine;

namespace SwordsMaker
{
    public class Manager : MonoBehaviour
    {
        public int nodeCount = 0;

        [System.Serializable]
        public class SpawnableObject
        {
            [Range(0f, 1f)] public float SpawnChance;

            public GameObject Prefab;
            public GameObject Required;
        }

        [SerializeField] private SpawnableObject[] objects;

        [SerializeField] private float[] spawnInterval;

        [SerializeField] private float range;
        [SerializeField] private float strZ;
        [SerializeField] private float endZ;

        public GameObject Panel;

        private void Start()
        {
            SpawnObjects();
        }

        void SpawnObjects()
        {
            float interval = Random.Range(spawnInterval[0], spawnInterval[1]);

            for (float z = strZ; z < endZ; z += interval)
            {
                Vector3 spawnPosition = new(Random.Range(-range, range), 0, z);

                SpawnRandomObject(spawnPosition);
            }
        }

        void SpawnRandomObject(Vector3 position)
        {
            int random = Random.Range(0, objects.Length);

            var spawnable = objects[random];
            if (Random.value <= spawnable.SpawnChance)
            {
                if (spawnable.Required == null || GameObject.Find(spawnable.Required.name) != null)
                {
                    GameObject @object = Instantiate(spawnable.Prefab, position, Quaternion.identity);

                    @object.name = spawnable.Prefab.name;

                    if (objects[random].SpawnChance > 0.4f)
                    {
                        switch (@object.name)
                        {
                            case "Node":
                                objects[random].SpawnChance -= 0.05f;
                                break;
                            case "Lava":
                                objects[random].SpawnChance -= 0.2f;
                                break;
                            case "Water":
                                objects[random].SpawnChance -= 0.2f;
                                break;
                            case "Press":
                                objects[random].SpawnChance -= 0.1f;
                                break;
                            case "Emery":
                                objects[random].SpawnChance -= 0.1f;
                                break;
                            case "Trap":
                                @object.transform.localScale = new Vector3(Random.Range(1.0f, 1.5f), @object.transform.localScale.y, @object.transform.localScale.z);
                                break;
                        }
                    }
                    return ;
                }
            }
            SpawnRandomObject(position);
        }

        public void Restart()
        {
            nodeCount = 0;

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
