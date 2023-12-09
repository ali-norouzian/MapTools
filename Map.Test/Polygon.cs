namespace Map.Test
{
	public class Polygon
	{
		public double[] Lats { get; }
		public double[] Lngs { get; }

		public Polygon(double[] lats, double[] lngs)
		{
			Lats = lats;
			Lngs = lngs;
		}

		public async Task<bool> IsPointInPolygon(double lat, double lon)
		{
			int i, j;
			bool inside = false;

			for (i = 0, j = Lats.Length - 1; i < Lats.Length; j = i++)
			{
				if (((Lngs[i] > lon) != (Lngs[j] > lon)) &&
					(lat < (Lats[j] - Lats[i]) * (lon - Lngs[i]) / (Lngs[j] - Lngs[i]) + Lats[i]))
				{
					inside = !inside;
				}
			}

			return inside;
		}
	}
}
