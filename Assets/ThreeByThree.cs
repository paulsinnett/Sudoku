using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThreeByThree : MonoBehaviour {

	public Cell cellTemplate;

	List<Cell>[] rows = new List<Cell>[3];
	List<Cell>[] columns  = new List<Cell>[3];

	void Start()
	{
		for (int i = 0; i < 3; ++i)
		{
			rows[i] = new List<Cell>();
			columns[i] = new List<Cell>();
		}

		for (int row = 0; row < 3; ++row)
		{
			for (int column = 0; column < 3; ++column)
			{
				Cell cell = Instantiate(cellTemplate);
				cell.transform.SetParent(transform);
                cell.transform.localScale = Vector3.one;

				rows[row].Add(cell);
				columns[column].Add(cell);
			}
		}
	}

	public List<Cell> GetRow (int row)
	{
		return rows[row];
	}

	public List<Cell> GetColumn (int column)
	{
		return columns[column];
	}

	public List<Cell> GetCells ()
	{
		return new List<Cell>(GetComponentsInChildren<Cell>());
	}
}
