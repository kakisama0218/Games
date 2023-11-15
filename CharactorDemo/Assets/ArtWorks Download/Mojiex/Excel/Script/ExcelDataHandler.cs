using System.Collections.Generic;
using System;
using UnityEngine;
using Mojiex;

[Serializable]
public class ExcelDataHandler
{
	public List<ArchipelagoData> m_ArchipelagoData = new List<ArchipelagoData>();
	public List<LocalizationData> m_LocalizationData = new List<LocalizationData>();
	public List<MapData> m_MapData = new List<MapData>();
}