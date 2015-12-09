using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sudoku : MonoBehaviour 
{
	bool solving = false;

	void Update () 
	{
		if (!solving && Input.GetKeyDown(KeyCode.Return))
		{
			solving = true;
			StartCoroutine(SolveSudoku());
		}
	}

	int Count(List<Cell> cells, int number)
	{
		int count = 0;
		foreach (Cell cell in cells)
		{
			if (cell.IsNumber(number))
			{
				count++;
			}
		}
		
		return count;
	}

	bool IsLegal(List<Cell> cells)
	{
		for (int i = 1; i <= 9; ++i)
		{
			if (Count(cells, i) > 1)
			{
				return false;
			}
		}
		
		return true;
	}

	bool IsLegal()
	{
		ThreeByThree[] allThreeByThrees = GetComponentsInChildren<ThreeByThree>();

		foreach (ThreeByThree threeByThree in allThreeByThrees)
		{
			if (!IsLegal(threeByThree.GetCells()))
			{
				return false;
			}
		}

		// check rows

		for (int i = 0; i < 9; i += 3)
		{
			for (int row = 0; row < 3; ++row)
			{
				List<Cell> nineCells = new List<Cell>();
				for (int j = 0; j < 3; ++j)
				{
					nineCells.AddRange(allThreeByThrees[i + j].GetRow(row));
				}

				if (!IsLegal(nineCells))
				{
					return false;
				}
			}
		}


		// check columns
		
		for (int j = 0; j < 3; ++j)
		{
			for (int column = 0; column < 3; ++column)
			{
				List<Cell> nineCells = new List<Cell>();
				for (int i = 0; i < 9; i += 3)
				{
					nineCells.AddRange(allThreeByThrees[i + j].GetColumn(column));
				}
				
				if (!IsLegal(nineCells))
				{
					return false;
				}
			}
		}

		return true;
	}

	IEnumerator SolveSudoku()
	{
		Debug.Log ("Solving...");

		yield return StartCoroutine(SolvePart ());

		if (GetEmpty() != null)
		{
			Debug.Log ("Fail");
		}

		Debug.Log ("Done!");
		solving = false;
	}

	Cell GetEmpty()
	{
		foreach (Cell cell in GetComponentsInChildren<Cell>())
		{
			if (cell.IsEmpty())
			{
				return cell;
			}
		}

		return null;
	}

	IEnumerator SolvePart()
	{
		Cell cell = GetEmpty();
		if (cell != null)
		{
			for (int i = 1; i <= 9; ++i)
			{
				cell.SetNumber(i);
				bool isLegal = IsLegal();
				if (isLegal)
				{
					yield return StartCoroutine(SolvePart ());
					if (GetEmpty() == null)
					{
						// done
						yield break;
					}
				}

				cell.Clear();
			}
		}
	}
}
