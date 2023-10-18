using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; // Required for SortingGroup

namespace Cainos.PixelArtTopDown_Basic
{
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;
        public string sortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);

            // Check and set the sorting layer for the main SpriteRenderer.
            SpriteRenderer mainSr = other.gameObject.GetComponent<SpriteRenderer>();
            if (mainSr)
            {
                mainSr.sortingLayerName = sortingLayer;
            }

            // Check and set the sorting layer for child SpriteRenderers.
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }

            // Check and set the sorting layer for SortingGroups.
            SortingGroup[] sgs = other.gameObject.GetComponentsInChildren<SortingGroup>();
            foreach (SortingGroup sg in sgs)
            {
                sg.sortingLayerName = sortingLayer;
            }
        }
    }
}