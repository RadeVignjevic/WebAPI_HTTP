namespace WebAPI_HTTP.Struct
{
    readonly struct StructGeoPoint
    {
        public string City { get; }

        public int X { get; }
        
        public int Y { get; }

        public StructGeoPoint(string _city,int _x, int _y)
        {
            City = _city;
            X = _x;
            Y = _y;
        }
    }
}
