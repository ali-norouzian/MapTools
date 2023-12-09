namespace Map.Test
{
	public class Circle
	{
		public double CenterLatitude { get; }
		public double CenterLongitude { get; }
		public double Radius { get; }

		public Circle(double centerLatitude, double centerLongitude, double radius)
		{
			CenterLatitude = centerLatitude;
			CenterLongitude = centerLongitude;
			Radius = radius;
		}

		public async Task<bool> IsPointInCircle(double lat, double lon)
		{
			var distance = await Distance(CenterLatitude, CenterLongitude, lat, lon);
			distance = await GeoCalculator.ConvertKmToMeters(distance);

			return distance <= Radius;
		}

		private async Task<double> Distance(double lat1, double lon1, double lat2, double lon2)
		{
			// Implement the Haversine formula or any other distance calculation method
			// to calculate the distance between two latitude and longitude points
			// and return the result.

			double distance = await GeoCalculator.Haversine(lat1, lon1, lat2, lon2);

			return distance;
		}



		private static class GeoCalculator
		{
			public const double EarthRadiusKm = 6371.0; // Radius of the Earth in kilometers

			public static double DegreesToRadians(double degrees)
			{
				return degrees * Math.PI / 180.0;
			}

			public static async Task<double> Haversine(double lat1, double lon1, double lat2, double lon2)
			{
				var dLat = DegreesToRadians(lat2 - lat1);
				var dLon = DegreesToRadians(lon2 - lon1);
				var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
						Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
						Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
				var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
				return EarthRadiusKm * c;
			}

			public static async Task<double> ConvertKmToMeters(double kilometers)
			{
				return kilometers * 1000.0; // Convert kilometers to meters
			}
		}
	}
}
