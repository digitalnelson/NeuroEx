using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileMedicine.MovementStudioForms
{
	// From:  http://msdn.microsoft.com/en-us/library/aa302371.aspx
	public class Permutation
	{
		private int[] data = null;
		private int order = 0;
		private int perm = 0;

		public int[] Idxs
		{
			get
			{
				int[] tmpData = new int[data.Length];
				data.CopyTo(tmpData, 0);
				return tmpData;
			}
		}

		public int N
		{
			get { return order; }
		}

		public int K
		{
			get { return perm; }
		}

		public Permutation(int n)
		{
			this.data = new int[n];
			for (int i = 0; i < n; ++i)
			{
				this.data[i] = i;
			}

			this.order = n;
		}

		public Permutation(int n, int k)
		{
			perm = k;

			this.data = new int[n];
			this.order = this.data.Length;

			// Step #1 - Find factoradic of k
			int[] factoradic = new int[n];

			for (int j = 1; j <= n; ++j)
			{
				factoradic[n - j] = k % j;
				k /= j;
			}

			// Step #2 - Convert factoradic to permuatation
			int[] temp = new int[n];

			for (int i = 0; i < n; ++i)
			{
				temp[i] = ++factoradic[i];
			}

			this.data[n - 1] = 1;  // right-most element is set to 1.

			for (int i = n - 2; i >= 0; --i)
			{
				this.data[i] = temp[i];
				for (int j = i + 1; j < n; ++j)
				{
					if (this.data[j] >= this.data[i])
						++this.data[j];
				}
			}

			for (int i = 0; i < n; ++i)  // put in 0-based form
			{
				--this.data[i];
			}

		}  // Permutation(n,k)

		public Permutation Successor()
		{
			Permutation result = new Permutation(this.order);

			int left, right;

			for (int k = 0; k < result.order; ++k)  // Step #0 - copy current data into result
			{
				result.data[k] = this.data[k];
			}

			left = result.order - 2;  // Step #1 - Find left value 
			while ((result.data[left] > result.data[left + 1]) && (left >= 1))
			{
				--left;
			}
			if ((left == 0) && (this.data[left] > this.data[left + 1]))
				return null;

			right = result.order - 1;  // Step #2 - find right; first value > left
			while (result.data[left] > result.data[right])
			{
				--right;
			}

			int temp = result.data[left];  // Step #3 - swap [left] and [right]
			result.data[left] = result.data[right];
			result.data[right] = temp;


			int i = left + 1;              // Step #4 - order the tail
			int j = result.order - 1;

			while (i < j)
			{
				temp = result.data[i];
				result.data[i++] = result.data[j];
				result.data[j--] = temp;
			}

			return result;
		}  // Successor()

		public override string ToString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("( ");
			for (int i = 0; i < this.order; ++i)
			{
				sb.Append(this.data[i].ToString() + " ");
			}
			sb.Append(")");

			return sb.ToString();
		}  // ToString()

	}  // class Permutation 
}
