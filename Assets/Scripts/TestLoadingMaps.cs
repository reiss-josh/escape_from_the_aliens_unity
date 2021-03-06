﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//GALVANI 1550,188,109, {75,90}, {169,45}
public class TestLoadingMaps : MonoBehaviour
{
    private RawImage img;
    public string startdir = "Assets/Maps/";
    public string filename = "galvani";
    public string fileext = ".jpg";

    public int upperBound = 1550;
    public int checkSize = 10;
    public Vector2Int hexOffset = new Vector2Int(188,109);
    public Vector2Int borderOffset = new Vector2Int(75,90);
    public int[,] MapArray = new int[23,14];

    // Start is called before the first frame update
    void Start()
    {
        //for displaying the output
        img = GetComponent<RawImage>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            var sourceTex = LoadPNG(startdir + filename + fileext);
            (img.texture, MapArray) = ConvertToMapArray(sourceTex, MapArray);
            //DebugLogArr2Dim<int>(MapArray);
        }
    }

    //takes a pixelarray, a center pixel, and image line width
    //checks a [checkSize x checkSize] square of pixels centered on currPix
    //returns one of four values:
    //0: black cell //1: grey cell
    //2: white cell //3: empty cell
    public int DetermineCellType(Color32[] pix, int currPix, int w)
    {
        bool seenGrey = false;
        bool seenWhite = false;

        for(int y = -checkSize; y <= checkSize; y++) {
            for(int x = -checkSize; x <= checkSize; x++){
                int currR = pix[currPix + x + y * w][0];
                if (currR < 10) return 0; //seen black: assume it's a black cell
                else if (currR > 210) seenWhite = true; //mark white
                else if (currR > 150) seenGrey = true; //mark if grey
            }
        }
        if (seenGrey && !seenWhite) return 1; //if grey only, grey
        if (seenWhite && !seenGrey) return 2; //if white only, white
        return 3; //if white and grey, empty cell
    }

    //loads a PNG from file as Texture2D
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(128, 128);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    //converts a Texture2D into a 2-D integer array
    //also draws debug info onto the passed texture
    public (Texture2D, int[,]) ConvertToMapArray(Texture2D sourceTex, int[,] MapArray)
    {
        var pix = sourceTex.GetPixels32();
        int width = sourceTex.width;
        for (int BRD = 0; BRD <= 1; BRD++)
        {
            for (int yReal = 0, yInd = borderOffset.y-(BRD * hexOffset.y / 2); yInd < upperBound; yInd += hexOffset.y)
            { 
                for (int xReal = BRD, xInd = borderOffset.x+(BRD*hexOffset.x/2); xInd < width; xInd += hexOffset.x)
                {
                    int cellType = DetermineCellType(pix, xInd + yInd * width, width);
                    MapArray[xReal, yReal] = cellType;
                    pix = DoodleAtPoint(pix, xInd, yInd, width, cellType, checkSize);
                    //Debug.Log(xReal + "," + yReal);
                    xReal +=2;
                }
                yReal++;
            }
        }
        // Copy the reversed image data to a new texture.
        var destTex = new Texture2D(sourceTex.width, sourceTex.height);
        destTex.SetPixels32(pix);
        destTex.Apply();
        return (destTex, MapArray);
    }

    //draw colored debugsquare
    public static Color32[] DoodleAtPoint(Color32[] pix, int xInd, int yInd, int width, int cellType, int rectSize = 0)
    {
        int currPix = xInd + yInd * width;

        //create colors
        var black   = new Color32(100, 255, 255, 255); //black cell
        var grey    = new Color32(255, 255, 100, 255); //grey cell
        var white   = new Color32(255, 100, 255, 255); //white cell
        var red     = new Color32(200, 200, 200, 255); //debug

        //set color based on celltype
        var chosencolor = red;
        if (cellType == 0) chosencolor = black;
        if (cellType == 1) chosencolor = grey;
        if (cellType == 2) chosencolor = white;

        //draw a square of color
        if (rectSize > 0)
        {
            for (int e = -rectSize; e <= rectSize; e++)
            {
                for (int d = -rectSize; d <= rectSize; d++)
                    pix[d + e * width + currPix] = chosencolor;
            }
        }

        return pix;
    }

    private void DebugLogArr2Dim <T>(T[,] printArr)
    {
        string[] result = new string[printArr.GetLength(0)];
        for(int i = 0; i < printArr.GetLength(0); i++)
        {
            string entry = "";
            for(int j = 0; j < printArr.GetLength(1); j++)
            {
                entry += "(" + i + "," + j + "): " + printArr[i, j] + "\n";
            }
            result[i] = entry;
        }
        for(int i = 0; i < result.Length; i++)
            Debug.Log(result[i]);
    }
}