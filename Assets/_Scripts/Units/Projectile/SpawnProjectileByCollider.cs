using UnityEngine;

public class SpawnProjectileByCollider : MonoBehaviour
{
    public LayerMask targetLayer; // Selecione a layer desejada no Inspector
    public int minimumObjectQty = 3; // Substitua 3 pelo n�mero desejado

    public GameObject objectToSpawn; // Substitua pelo objeto que voc� quer spawnar
    public Transform spawnPoint; // Posi��o onde o novo objeto ser� spawnado

    public int currentObjQty;

    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D n�o encontrado neste GameObject.");
        }
    }

    void Update()
    {
        if (boxCollider == null)
        {
            // Adicione uma l�gica aqui para lidar com o caso em que o BoxCollider2D n�o foi encontrado
            return;
        }

        Collider2D[] objetosDetectados = new Collider2D[5]; // Ajuste o tamanho do array conforme necess�rio
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = targetLayer;

        currentObjQty = Physics2D.OverlapCollider(boxCollider, filter, objetosDetectados);

        if (currentObjQty < minimumObjectQty)
        {
            
            SpawnNovoObjeto();
        }
    }

    void SpawnNovoObjeto()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    // Para visualizar o collider no Editor, descomente as linhas abaixo:
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
