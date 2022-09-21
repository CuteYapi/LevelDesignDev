using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCameraViewModel : MonoBehaviour
{
    float width;
    float height;
    int layerMask;

    RaycastHit[] RaycastAnchor = new RaycastHit[4];

    void Start()
    {
        height = Camera.main.orthographicSize * CommonValueData.I.MapRespawnBorder;
        width = height * Camera.main.aspect * CommonValueData.I.MapRespawnBorder;

        layerMask = 1 << LayerMask.NameToLayer("Background");
        StartCoroutine(MoveBackground());
    }

    public IEnumerator MoveBackground()
    {
        while (true)
        {
            #region DrawRay
            Debug.DrawRay(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(width, height, transform.localPosition.z), transform.forward * 20, Color.blue);
            Debug.DrawRay(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(-width, height, transform.localPosition.z), transform.forward * 20, Color.blue);
            Debug.DrawRay(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(-width, -height, transform.localPosition.z), transform.forward * 20, Color.blue);
            Debug.DrawRay(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(width, -height, transform.localPosition.z), transform.forward * 20, Color.blue);
            #endregion

            Physics.Raycast(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(width, height, -3), transform.forward, out RaycastAnchor[0], 20, layerMask);
            Physics.Raycast(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(-width, height, -3), transform.forward, out RaycastAnchor[1], 20, layerMask);
            Physics.Raycast(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(-width, -height, -3), transform.forward, out RaycastAnchor[2], 20, layerMask);
            Physics.Raycast(PlayerCharacterViewModel.I.transform.localPosition + new Vector3(width, -height, -3), transform.forward, out RaycastAnchor[3], 20, layerMask);

            //playerVector = (0,1)
            if (!RaycastAnchor[0].transform && !RaycastAnchor[1].transform)
            {
                List<GameObject> moveGameObjects = new List<GameObject>();

                float min_y = BackgroundViewModel.I.BackgroundView.Min(x => x.transform.localPosition.y);
                float max_y = BackgroundViewModel.I.BackgroundView.Max(x => x.transform.localPosition.y);

                moveGameObjects = BackgroundViewModel.I.BackgroundView.Where(x => x.transform.localPosition.y <= min_y + 1f).ToList();
                foreach (GameObject gameObject in moveGameObjects)
                {
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, max_y + 12.5f, 0);
                }
            }

            //playerVector = (0,-1)
            else if (!RaycastAnchor[2].transform && !RaycastAnchor[3].transform)
            {
                List<GameObject> moveGameObjects = new List<GameObject>();

                float min_y = BackgroundViewModel.I.BackgroundView.Min(x => x.transform.localPosition.y);
                float max_y = BackgroundViewModel.I.BackgroundView.Max(x => x.transform.localPosition.y);

                moveGameObjects = BackgroundViewModel.I.BackgroundView.Where(x => x.transform.localPosition.y >= max_y - 1f).ToList();
                foreach (GameObject gameObject in moveGameObjects)
                {
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, min_y - 12.5f, 0);
                }
            }

            //playerVector = (1,0)
            if (!RaycastAnchor[0].transform && !RaycastAnchor[3].transform)
            {
                List<GameObject> moveGameObjects = new List<GameObject>();

                float min_x = BackgroundViewModel.I.BackgroundView.Min(x => x.transform.localPosition.x);
                float max_x = BackgroundViewModel.I.BackgroundView.Max(x => x.transform.localPosition.x);

                moveGameObjects = BackgroundViewModel.I.BackgroundView.Where(x => x.transform.localPosition.x <= min_x + 1f).ToList();
                foreach (GameObject gameObject in moveGameObjects)
                {
                    gameObject.transform.localPosition = new Vector3(max_x + 12.5f, gameObject.transform.localPosition.y, 0);
                }
            }

            //playerVector = (-1,0)
            else if (!RaycastAnchor[1].transform && !RaycastAnchor[2].transform)
            {
                List<GameObject> moveGameObjects = new List<GameObject>();

                float min_x = BackgroundViewModel.I.BackgroundView.Min(x => x.transform.localPosition.x);
                float max_x = BackgroundViewModel.I.BackgroundView.Max(x => x.transform.localPosition.x);

                moveGameObjects = BackgroundViewModel.I.BackgroundView.Where(x => x.transform.localPosition.x >= max_x - 1f).ToList();
                foreach (GameObject gameObject in moveGameObjects)
                {
                    gameObject.transform.localPosition = new Vector3(min_x - 12.5f, gameObject.transform.localPosition.y, 0);
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
