using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ATMQueueManager : MonoBehaviour
{
    public Transform[] queuePositions; // Positions in front of the ATM
    public List<GameObject> characters = new List<GameObject>(); // Characters in queue
    public Transform atmPosition; // ATM entry position
    public List<GameObject> characterPrefab; // Prefab for new characters

    public bool IsFree = true;

    void Awake()
    {
       
   
    
    }

    void OnDestroy()
    {
      
    
    }

   public void ProcessQueue()
    {
        if (characters.Count == 0 || !IsFree) return; // No one in the queue

        // Move the first character to ATM and remove it
        // Remove first character from the list
        //removecharacter();
        GameObject firstCharacter = characters[0];
        StartCoroutine(MoveToPosition(firstCharacter, atmPosition.position, () =>
        {
            //  Destroy(firstCharacter); // Remove character after reaching ATM
        }));
        characters.RemoveAt(0);
        // Move the remaining characters forward
        for (int i = 0; i < characters.Count; i++)
        {
            StartCoroutine(MoveToPosition(characters[i], queuePositions[i].position, null));
        }

        // Spawn a new character at the last position (index 2)
        SpawnNewCharacter();

    }
    public void removecharacter()
    {
     

        characters.RemoveAt(0);
    }
   public bool canpress = true;
    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && canpress)
        {
           canpress = false;
            ProcessQueue();
        }
       
    }

    void SpawnNewCharacter()
    {
        GameObject newCharacter = Instantiate(characterPrefab[Random.Range(0,characterPrefab.Count)], queuePositions[2].position, queuePositions[2].rotation);
        characters.Add(newCharacter); // Add to the end of the queue
    }

    IEnumerator MoveToPosition(GameObject character, Vector3 targetPos, System.Action onComplete)
    {
        float duration = 1.0f;
        float elapsed = 0;
        Vector3 startPos = character.transform.position;

        while (elapsed < duration)
        {
            character.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        character.transform.position = targetPos;
        onComplete?.Invoke();
    }
}

