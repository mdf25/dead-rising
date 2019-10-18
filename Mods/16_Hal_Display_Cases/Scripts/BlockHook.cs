
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHook : BlockSecureLoot
{
    
    private Dictionary<int, GameObject> Objects = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> Meshes = new Dictionary<int, GameObject>();

    public override void OnBlockEntityTransformAfterActivated(WorldBase _world, Vector3i _blockPos, int _cIdx, BlockValue _blockValue, BlockEntityData _ebcd)
    {
        base.OnBlockEntityTransformAfterActivated(_world, _blockPos, _cIdx, _blockValue, _ebcd);

        var hash = _blockPos.GetHashCode();

        if (!Objects.ContainsKey(hash))
            Objects.Add(hash, null);
        Objects[hash] = _ebcd.transform.gameObject;
        //Debug.Log("Gameobject: "+ (gameObject == null ? "null" : gameObject.name));
        //  script = gameObject.AddComponent<TurretScript>();

        //Debug.Log("After Activated");
        if (!GameManager.IsDedicatedServer) //only run on SP
            GameManager.Instance.StartCoroutine(checkCase(_blockPos, 0.5f));

    }

    public override bool OnBlockActivated(int _indexInBlockActivationCommands, WorldBase _world, int _cIdx, Vector3i _blockPos, BlockValue _blockValue, EntityAlive _player)
    {

        var hash = _blockPos.GetHashCode();
        var gameObject = Objects[hash];
        var hasMesh = Meshes.ContainsKey(hash);
        var pickup = gameObject != null && hasMesh;

      //  Log.Out("Block activated");
        
        if (pickup)
        {
            Vector3i v = new Vector3i(gameObject.transform.position);
            if (gameObject.transform.position.x < 0)
                v.x -= 1;
            if (gameObject.transform.position.z < 0)
                v.z -= 1;
            TileEntitySecureLootContainer secureLootContainer = null;
            for (int n = 0; n < GameManager.Instance.World.ChunkClusters.Count; n++)
            {
                secureLootContainer = (TileEntitySecureLootContainer) GameManager.Instance.World.GetTileEntity(n, v);
                if (secureLootContainer != null)
                    break;

            }

            if (secureLootContainer != null)
            {
                var item = secureLootContainer.items[0];
                if (item.itemValue.type != 0)
                {
                    if (GameManager.Instance.World.GetPrimaryPlayer().bag.AddItem(item))
                    {
                        GameManager.Instance.World.GetPrimaryPlayer().AddUIHarvestingItem(item, false);
                        secureLootContainer.items[0] = ItemStack.Empty;
                        if (!GameManager.IsDedicatedServer) //only run on SP
                            GameManager.Instance.StartCoroutine(checkCase(_blockPos, 0 ));
                        return true;
                    }
                }
            }
        }

        if (!GameManager.IsDedicatedServer) //only run on SP
            GameManager.Instance.StartCoroutine(checkCase(_blockPos, 0.5f));

        return base.OnBlockActivated(_indexInBlockActivationCommands, _world, _cIdx, _blockPos, _blockValue, _player);
    }



    IEnumerator checkCase(Vector3i _blockPos, float delay)
    {

        //Debug.Log("Checking for window close at " + _blockPos +  " delay: " + delay);
        WaitForSeconds ret = new WaitForSeconds(0.25f);

        var hash = _blockPos.GetHashCode();

        var gameObject = Objects[hash];

        yield return new WaitForSeconds(delay);
        var ui = LocalPlayerUI.GetUIForPlayer(GameManager.Instance.World.GetPrimaryPlayer());

        //ui.windowManager.IsWindowOpen("dragAndDrop") || 
        while (ui.windowManager.IsWindowOpen("looting"))
        {
            //Debug.Log("Window is open...");
            yield return ret;
        }

        // Debug.Log("Window closed...");
        
        Vector3i v = _blockPos;

        var block = GameManager.Instance.World.GetBlock(_blockPos);
        

        TileEntitySecureLootContainer secureLootContainer = null;
        for (int n = 0; n < GameManager.Instance.World.ChunkClusters.Count; n++)
        {
            secureLootContainer = (TileEntitySecureLootContainer)GameManager.Instance.World.GetTileEntity(n, v);
            if (secureLootContainer != null)
                break;

        }

        int id = -1;
        string steamID = "";
        if (secureLootContainer == null)
        {
            Debug.Log("Loot null");
            yield break;
        }



        if (Meshes.ContainsKey(hash))
        {
            var meshToDestroy = Meshes[hash];

            if (meshToDestroy != null)
            {
                //Debug.Log("Destroying mesh: " + meshToDestroy.name);
                GameObject.Destroy(meshToDestroy);
                Meshes[hash] = null;
            }
        }
        
        var item = secureLootContainer.items[0];
        if (item == null || item.itemValue == null || item.itemValue.ItemClass == null)
        {
           //Debug.Log("Item null");
            yield break;
        }

        if (String.IsNullOrEmpty(item.itemValue.ItemClass.MeshFile))
        {
            //Debug.Log("Mesh empty");
            yield break;
        }
        var prefab = Resources.Load(item.itemValue.ItemClass.MeshFile); 

        
        if (prefab == null)
        {
            //prefab = SDX.Payload.ResourceWrapper.Load1P(item.itemValue.ItemClass.MeshFile);
           Debug.Log("GO Null");
        }
        
        if (prefab != null)
        {

            var mesh = GameObject.Instantiate(prefab) as GameObject;
         
            if (!Meshes.ContainsKey(hash))
            {
               Meshes.Add(hash, null);
            }

            Meshes[hash] = mesh;

            Vector3 rotation = Vector3.zero;

            Vector3 size = Vector3.zero; //CalculateBoundingBox(mesh).size; // 
            var rend = mesh.GetComponent<MeshRenderer>() ?? mesh.GetComponentInChildren<MeshRenderer>();
            
            if (rend == null)
            {
                var skin = mesh.GetComponentInChildren<SkinnedMeshRenderer>();
                if (skin != null)
                {
                    size = skin.bounds.size;
                }
                else
                {
                    ListTransforms(mesh.transform, "");
                  Debug.Log("Mesh missing");
                }
            }
            else
            {
                size = rend.bounds.size;
            }

            rotation = gameObject.transform.rotation.eulerAngles + mesh.transform.rotation.eulerAngles;

          //  Debug.Log("Bounds: " + size + " Rotation: "+ mesh.transform.rotation.eulerAngles + " - Base Rotation: " + gameObject.transform.rotation.eulerAngles +  "       class: " + item.itemValue.ItemClass.Properties.GetString("Class"));

            if (size.x > size.y && size.x > size.z)
            {
               // Debug.Log("Natural X");
                if (size.z > size.y)
                {
               //    Debug.Log("Rotate y **");
                    if (!Input.GetKey(KeyCode.LeftShift))
                        rotation += new Vector3(0, 90, 0);
                }
            }
            else
            {
                if (size.z > size.x)
                {
                  //  Debug.Log("Rotate y");
                    if (!Input.GetKey(KeyCode.LeftShift))
                        rotation += new Vector3(0, 90, 0);

                    //if (size.y > size.x && size.y > size.z)
                    //{
                    //    Debug.Log("Rotate taller 1");
                    //    rotation += new Vector3(90, 0, 0);
                    //}
                }
                else if (size.y > size.x && size.y > size.z)
                {
                  //  Debug.Log("Rotate taller 2");
                    rotation += new Vector3(0, 0, 270);
                }

                if (size.x > size.y + 0.1f)
                {
                 //   Debug.Log("Rotate z from y. " + (size.x) + ">" + size.y);
                    if (!Input.GetKey(KeyCode.LeftShift))
                        rotation += new Vector3(0, 0, 90);
                }

            }


            var rotateX = block.Block.Properties.GetInt("RotateX");
            var rotateY = block.Block.Properties.GetInt("RotateY");
            var rotateZ = block.Block.Properties.GetInt("RotateZ");
            //Log.Out("Rots " + rotateX + ", " + rotateY + "," + rotateZ);
            rotation += new Vector3(rotateX, rotateY, rotateZ);
            
            //if (Input.GetKey(KeyCode.Keypad1))
            //{
            //    Log.Out("Force x");
            //    rotation += new Vector3(90, 0, 0);
            //}
            //if (Input.GetKey(KeyCode.Keypad2))
            //{
            //    Log.Out("Force y");
            //    rotation += new Vector3(0, 90, 0);
            //}
            //if (Input.GetKey(KeyCode.Keypad3))
            //{
            //    Log.Out("Force z");
            //    rotation += new Vector3(0, 0, 90);
            //}
            //if (Input.GetKey(KeyCode.Keypad5))
            //{
            //    ListTransforms(mesh.transform, "");
            //}

            Vector3 forward = Vector3.zero;

            var baseRotation = Mathf.Round(gameObject.transform.rotation.eulerAngles.y);
            if (baseRotation == 0)
            {
                forward.z = 0.2f;
            }
            else if (baseRotation == 90f)
            {
                forward.x = -0.3f;
                forward.z += 0.5f;
            }
            else if (baseRotation == 180f)
                forward.z = 0.8f;
            else if (baseRotation == 270f)
            {
                forward.x = 0.2f;
                forward.z += 0.5f;
            }
            
            mesh.transform.rotation = Quaternion.Euler(rotation);
            mesh.transform.position = new Vector3(v.x + 0.5f, v.y + 0.5f, v.z) + forward - Origin.position;
            mesh.transform.parent = gameObject.transform;
            //Debug.Log("Mesh created " + v + " forward: " +forward);
        }

    }
    
    private void ListTransforms(Transform t, string parentList)
    {
        parentList += t.name + "->";
        Debug.Log(parentList);

        var comps = t.GetComponents<Component>();

        //foreach (var c in comps)
        //{
        //    Debug.Log("Comp: " + c.name + " - " + c.GetType().Name);
        //}

        foreach (Transform c in t)
            ListTransforms(c, parentList);

    }

    public override void OnBlockRemoved(WorldBase world, Chunk _chunk, Vector3i _blockPos, BlockValue _blockValue)
    {
        var hash = _blockPos.GetHashCode();
        if (Meshes.ContainsKey(hash))
        {
            if (Meshes[hash] != null)
            {
                GameObject.Destroy(Meshes[hash]);
            }
            Meshes[hash] = null;
        }
        base.OnBlockRemoved(world, _chunk, _blockPos, _blockValue);

    }

    public override void OnBlockLoaded(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        base.OnBlockLoaded(_world, _clrIdx, _blockPos, _blockValue);
    }

    public override void OnBlockAdded(WorldBase world, Chunk _chunk, Vector3i _blockPos, BlockValue _blockValue)
    {
        base.OnBlockAdded(world, _chunk, _blockPos, _blockValue);

    }

    public override void PlaceBlock(WorldBase _world, BlockPlacement.Result _result, EntityAlive _ea)
    {
        base.PlaceBlock(_world, _result, _ea);
    }


    public override void OnBlockValueChanged(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _oldBlockValue, BlockValue _newBlockValue)
    {
        base.OnBlockValueChanged(_world, _clrIdx, _blockPos, _oldBlockValue, _newBlockValue);
    }
    public override void OnBlockUnloaded(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        var hash = _blockPos.GetHashCode();
        if (Meshes.ContainsKey(hash))
        {
            if (Meshes[hash] != null)
            {
                GameObject.Destroy(Meshes[hash]);
            }
            Meshes[hash] = null;

        }
        base.OnBlockUnloaded(_world, _clrIdx, _blockPos, _blockValue);
       
    }


}

