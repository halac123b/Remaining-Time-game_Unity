using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] UnityEngine.UI.Image Sampleimage, Character;
  [SerializeField] RawImage hueImage;
  [SerializeField] Slider hueSlider;
  [SerializeField] Button SetColor;

  // [SerializeField] Shader charactor;
  private Texture2D hueTexture;
  private void Awake()
  {
    // charactor = GameObject.Find("Container").GetComponentInChildren<Material>() ;   
    SetColor.onClick.AddListener(() =>
    {
      LobbyManager.Instance.UpdatePlayerColor(Sampleimage.color);

      Debug.Log("SetColor");
    });
  }
  void Start()
  {
    CreateHueImage();
  }

  private void CreateHueImage()
  {
    hueTexture = new Texture2D(160, 1);
    hueTexture.wrapMode = TextureWrapMode.Clamp;
    hueTexture.name = "HueTexture";

    for (int i = 0; i < hueTexture.width; i++)
    {
      hueTexture.SetPixel(i, 0, Color.HSVToRGB((float)i / hueTexture.width, 1, 1f));
    }
    hueTexture.Apply();
    // currentHue = 0;
    hueImage.texture = hueTexture;
  }
  public void colorPickerControler(float hueValue)
  {
    Color newColor = Color.HSVToRGB(hueValue, 1, 1);
    Sampleimage.color = newColor;
    Character.material.color = newColor;
  }
  // Update is called once per frame
  void Update()
  {
    colorPickerControler(hueSlider.value);
    // charactor.color = Sampleimage.color;
  }


}
