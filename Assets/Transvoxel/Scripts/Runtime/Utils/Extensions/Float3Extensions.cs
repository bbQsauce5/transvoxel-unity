using Unity.Mathematics;

public static class Float3Extensions
{
    private static readonly float _tolerance = .0001f;
    public static bool Approx(this float3 value, in float3 other) {
        var difference = math.abs(value - other);
        return difference.x < _tolerance && 
               difference.y < _tolerance && 
               difference.z < _tolerance;
    }
}
