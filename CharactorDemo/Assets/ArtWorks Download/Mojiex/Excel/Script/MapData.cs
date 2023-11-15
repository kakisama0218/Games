using System.Data;
using System;
using Mojiex;

[Serializable]
public class MapData : IExcelData
{
	public int Id;
	public int Level;
	public string PictureName;
	public int[] Data;
	public void FillData(DataRow data)
	{
		Id = int.Parse(data[0].ToString());
		Level = int.Parse(data[1].ToString());
		PictureName = data[2].ToString();
		Data = data[3].ToString().SplitByComma().ToInts();
	}
}