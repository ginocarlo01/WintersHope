To take object from the pool, you must:

-have a pool in the scene
-find the object pooler in the start:
ObjectPooler objectPooler;
objectPooler = ObjectPooler.instance;
-and use this function instead of instantiate:
GameObject newProjectile = objectPooler.SpawnFromPool(objectPoolTag.ToString(), spawnPositionProjectile.position, Quaternion.identity);

Now, in the other side, the object from the pool must:

-import from IPooledObject
-implement these two Functions: OnObjectDisabled() and OnObjectSpawn()
-this should be called then the object is disabled: this.gameObject.SetActive(false);

To destroy the object from the pool:
#region DisableProjectile
IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

if (objectFromPool != null)
{
    objectFromPool.OnObjectDisabled();
}
#endregion