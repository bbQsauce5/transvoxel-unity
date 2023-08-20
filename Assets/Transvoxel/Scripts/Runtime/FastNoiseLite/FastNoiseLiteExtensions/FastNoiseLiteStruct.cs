// MIT License
//
// Copyright(c) 2020 Jordan Peck (jordan.me2@gmail.com)
// Copyright(c) 2020 Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// .'',;:cldxkO00KKXXNNWWWNNXKOkxdollcc::::::;:::ccllloooolllllllllooollc:,'...        ...........',;cldxkO000Okxdlc::;;;,,;;;::cclllllll
// ..',;:ldxO0KXXNNNNNNNNXXK0kxdolcc::::::;;;,,,,,,;;;;;;;;;;:::cclllllc:;'....       ...........',;:ldxO0KXXXK0Okxdolc::;;;;::cllodddddo
// ...',:loxO0KXNNNNNXXKK0Okxdolc::;::::::::;;;,,'''''.....''',;:clllllc:;,'............''''''''',;:loxO0KXNNNNNXK0Okxdollccccllodxxxxxxd
// ....';:ldkO0KXXXKK00Okxdolcc:;;;;;::cclllcc:;;,''..... ....',;clooddolcc:;;;;,,;;;;;::::;;;;;;:cloxk0KXNWWWWWWNXKK0Okxddoooddxxkkkkkxx
// .....';:ldxkOOOOOkxxdolcc:;;;,,,;;:cllooooolcc:;'...      ..,:codxkkkxddooollloooooooollcc:::::clodkO0KXNWWWWWWNNXK00Okxxxxxxxxkkkkxxx
// . ....';:cloddddo___________,,,,;;:clooddddoolc:,...      ..,:ldx__00OOOkkk___kkkkkkxxdollc::::cclodkO0KXXNNNNNNXXK0OOkxxxxxxxxxxxxddd
// .......',;:cccc:|           |,,,;;:cclooddddoll:;'..     ..';cox|  \KKK000|   |KK00OOkxdocc___;::clldxxkO0KKKKK00Okkxdddddddddddddddoo
// .......'',,,,,''|   ________|',,;;::cclloooooolc:;'......___:ldk|   \KK000|   |XKKK0Okxolc|   |;;::cclodxxkkkkxxdoolllcclllooodddooooo
// ''......''''....|   |  ....'',,,,;;;::cclloooollc:;,''.'|   |oxk|    \OOO0|   |KKK00Oxdoll|___|;;;;;::ccllllllcc::;;,,;;;:cclloooooooo
// ;;,''.......... |   |_____',,;;;____:___cllo________.___|   |___|     \xkk|   |KK_______ool___:::;________;;;_______...'',;;:ccclllloo
// c:;,''......... |         |:::/     '   |lo/        |           |      \dx|   |0/       \d|   |cc/        |'/       \......',,;;:ccllo
// ol:;,'..........|    _____|ll/    __    |o/   ______|____    ___|   |   \o|   |/   ___   \|   |o/   ______|/   ___   \ .......'',;:clo
// dlc;,...........|   |::clooo|    /  |   |x\___   \KXKKK0|   |dol|   |\   \|   |   |   |   |   |d\___   \..|   |  /   /       ....',:cl
// xoc;'...  .....'|   |llodddd|    \__|   |_____\   \KKK0O|   |lc:|   |'\       |   |___|   |   |_____\   \.|   |_/___/...      ...',;:c
// dlc;'... ....',;|   |oddddddo\          |          |Okkx|   |::;|   |..\      |\         /|   |          | \         |...    ....',;:c
// ol:,'.......',:c|___|xxxddollc\_____,___|_________/ddoll|___|,,,|___|...\_____|:\ ______/l|___|_________/...\________|'........',;::cc
// c:;'.......';:codxxkkkkxxolc::;::clodxkOO0OOkkxdollc::;;,,''''',,,,''''''''''',,'''''',;:loxkkOOkxol:;,'''',,;:ccllcc:;,'''''',;::ccll
// ;,'.......',:codxkOO0OOkxdlc:;,,;;:cldxxkkxxdolc:;;,,''.....'',;;:::;;,,,'''''........,;cldkO0KK0Okdoc::;;::cloodddoolc:;;;;;::ccllooo
// .........',;:lodxOO0000Okdoc:,,',,;:clloddoolc:;,''.......'',;:clooollc:;;,,''.......',:ldkOKXNNXX0Oxdolllloddxxxxxxdolccccccllooodddd
// .    .....';:cldxkO0000Okxol:;,''',,;::cccc:;,,'.......'',;:cldxxkkxxdolc:;;,'.......';coxOKXNWWWNXKOkxddddxxkkkkkkxdoollllooddxxxxkkk
//       ....',;:codxkO000OOxdoc:;,''',,,;;;;,''.......',,;:clodkO00000Okxolc::;,,''..',;:ldxOKXNWWWNNK0OkkkkkkkkkkkxxddooooodxxkOOOOO000
//       ....',;;clodxkkOOOkkdolc:;,,,,,,,,'..........,;:clodxkO0KKXKK0Okxdolcc::;;,,,;;:codkO0XXNNNNXKK0OOOOOkkkkxxdoollloodxkO0KKKXXXXX
//
// VERSION: 1.0.1
// https://github.com/Auburn/FastNoise

using System;
using System.Runtime.CompilerServices;

// Switch between using floats or doubles for input position
using FNLfloat = System.Single;
//using FNLfloat = System.Double;

public struct FastNoiseLiteStruct
{
    private const short INLINE = 256; // MethodImplOptions.AggressiveInlining;
    private const short OPTIMISE = 512; // MethodImplOptions.AggressiveOptimization;

    public enum NoiseType
    {
        OpenSimplex2,
        OpenSimplex2S,
        Cellular,
        Perlin,
        ValueCubic,
        Value
    };
    public enum RotationType3D
    {
        None,
        ImproveXYPlanes,
        ImproveXZPlanes
    };
    public enum FractalType
    {
        None,
        FBm,
        Ridged,
        PingPong,
        DomainWarpProgressive,
        DomainWarpIndependent
    };
    public enum CellularDistanceFunction
    {
        Euclidean,
        EuclideanSq,
        Manhattan,
        Hybrid
    };
    public enum CellularReturnType
    {
        CellValue,
        Distance,
        Distance2,
        Distance2Add,
        Distance2Sub,
        Distance2Mul,
        Distance2Div
    };
    public enum DomainWarpType
    {
        OpenSimplex2,
        OpenSimplex2Reduced,
        BasicGrid
    };
    private enum TransformType3D
    {
        None,
        ImproveXYPlanes,
        ImproveXZPlanes,
        DefaultOpenSimplex2
    };

    public int _seed;
    public float _frequency;
    public NoiseType _noiseType;
    private RotationType3D _rotationType3D;
    private TransformType3D _transformType3D;

    private FractalType _fractalType;
    private int _octaves;
    private float _lacunarity;
    private float _gain;
    private float _weightedStrength;
    private float _pingPongStrength;

    private float _fractalBounding;

    private CellularDistanceFunction _cellularDistanceFunction;
    private CellularReturnType _cellularReturnType;
    private float _cellularJitterModifier;

    private DomainWarpType _domainWarpType;
    private TransformType3D _warpTransformType3D;
    private float _domainWarpAmp;

    /// <summary>
    /// Sets seed used for all noise types
    /// </summary>
    /// <remarks>
    /// Default: 1337
    /// </remarks>
    public void SetSeed(int seed) { _seed = seed; }

    /// <summary>
    /// Sets frequency for all noise types
    /// </summary>
    /// <remarks>
    /// Default: 0.01
    /// </remarks>
    public void SetFrequency(float frequency) { _frequency = frequency; }

    /// <summary>
    /// Sets noise algorithm used for GetNoise(...)
    /// </summary>
    /// <remarks>
    /// Default: OpenSimplex2
    /// </remarks>
    public void SetNoiseType(NoiseType noiseType)
    {
        _noiseType = noiseType;
        UpdateTransformType3D();
    }

    /// <summary>
    /// Sets domain rotation type for 3D Noise and 3D DomainWarp.
    /// Can aid in reducing directional artifacts when sampling a 2D plane in 3D
    /// </summary>
    /// <remarks>
    /// Default: None
    /// </remarks>
    public void SetRotationType3D(RotationType3D rotationType3D)
    {
        this._rotationType3D = rotationType3D;
        UpdateTransformType3D();
        UpdateWarpTransformType3D();
    }

    /// <summary>
    /// Sets method for combining octaves in all fractal noise types
    /// </summary>
    /// <remarks>
    /// Default: None
    /// Note: FractalType.DomainWarp... only affects DomainWarp(...)
    /// </remarks>
    public void SetFractalType(FractalType fractalType) { _fractalType = fractalType; }

    /// <summary>
    /// Sets octave count for all fractal noise types 
    /// </summary>
    /// <remarks>
    /// Default: 3
    /// </remarks>
    public void SetFractalOctaves(int octaves)
    {
        _octaves = octaves;
        CalculateFractalBounding();
    }

    /// <summary>
    /// Sets octave lacunarity for all fractal noise types
    /// </summary>
    /// <remarks>
    /// Default: 2.0
    /// </remarks>
    public void SetFractalLacunarity(float lacunarity) { _lacunarity = lacunarity; }

    /// <summary>
    /// Sets octave gain for all fractal noise types
    /// </summary>
    /// <remarks>
    /// Default: 0.5
    /// </remarks>
    public void SetFractalGain(float gain)
    {
        _gain = gain;
        CalculateFractalBounding();
    }

    /// <summary>
    /// Sets octave weighting for all none DomainWarp fratal types
    /// </summary>
    /// <remarks>
    /// Default: 0.0
    /// Note: Keep between 0...1 to maintain -1...1 output bounding
    /// </remarks>
    public void SetFractalWeightedStrength(float weightedStrength) { this._weightedStrength = weightedStrength; }

    /// <summary>
    /// Sets strength of the fractal ping pong effect
    /// </summary>
    /// <remarks>
    /// Default: 2.0
    /// </remarks>
    public void SetFractalPingPongStrength(float pingPongStrength) { _pingPongStrength = pingPongStrength; }


    /// <summary>
    /// Sets distance function used in cellular noise calculations
    /// </summary>
    /// <remarks>
    /// Default: Distance
    /// </remarks>
    public void SetCellularDistanceFunction(CellularDistanceFunction cellularDistanceFunction) { _cellularDistanceFunction = cellularDistanceFunction; }

    /// <summary>
    /// Sets return type from cellular noise calculations
    /// </summary>
    /// <remarks>
    /// Default: EuclideanSq
    /// </remarks>
    public void SetCellularReturnType(CellularReturnType cellularReturnType) { _cellularReturnType = cellularReturnType; }

    /// <summary>
    /// Sets the maximum distance a cellular point can move from it's grid position
    /// </summary>
    /// <remarks>
    /// Default: 1.0
    /// Note: Setting this higher than 1 will cause artifacts
    /// </remarks> 
    public void SetCellularJitter(float cellularJitter) { _cellularJitterModifier = cellularJitter; }


    /// <summary>
    /// Sets the warp algorithm when using DomainWarp(...)
    /// </summary>
    /// <remarks>
    /// Default: OpenSimplex2
    /// </remarks>
    public void SetDomainWarpType(DomainWarpType domainWarpType)
    {
        _domainWarpType = domainWarpType;
        UpdateWarpTransformType3D();
    }


    /// <summary>
    /// Sets the maximum warp distance from original position when using DomainWarp(...)
    /// </summary>
    /// <remarks>
    /// Default: 1.0
    /// </remarks>
    public void SetDomainWarpAmp(float domainWarpAmp) { _domainWarpAmp = domainWarpAmp; }


    /// <summary>
    /// 2D noise at given position using current settings
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(OPTIMISE)]
    public float GetNoise(FNLfloat x, FNLfloat y)
    {
        TransformNoiseCoordinate(ref x, ref y);

        switch (_fractalType)
        {
            default:
                return GenNoiseSingle(_seed, x, y);
            case FractalType.FBm:
                return GenFractalFBm(x, y);
            case FractalType.Ridged:
                return GenFractalRidged(x, y);
            case FractalType.PingPong:
                return GenFractalPingPong(x, y);
        }
    }

    /// <summary>
    /// 3D noise at given position using current settings
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(OPTIMISE)]
    public float GetNoise(FNLfloat x, FNLfloat y, FNLfloat z)
    {
        TransformNoiseCoordinate(ref x, ref y, ref z);

        switch (_fractalType)
        {
            default:
                return GenNoiseSingle(_seed, x, y, z);
            case FractalType.FBm:
                return GenFractalFBm(x, y, z);
            case FractalType.Ridged:
                return GenFractalRidged(x, y, z);
            case FractalType.PingPong:
                return GenFractalPingPong(x, y, z);
        }
    }


    /// <summary>
    /// 2D warps the input position using current domain warp settings
    /// </summary>
    /// <example>
    /// Example usage with GetNoise
    /// <code>DomainWarp(ref x, ref y)
    /// noise = GetNoise(x, y)</code>
    /// </example>
    [MethodImpl(OPTIMISE)]
    public void DomainWarp(ref FNLfloat x, ref FNLfloat y)
    {
        switch (_fractalType)
        {
            default:
                DomainWarpSingle(ref x, ref y);
                break;
            case FractalType.DomainWarpProgressive:
                DomainWarpFractalProgressive(ref x, ref y);
                break;
            case FractalType.DomainWarpIndependent:
                DomainWarpFractalIndependent(ref x, ref y);
                break;
        }
    }

    /// <summary>
    /// 3D warps the input position using current domain warp settings
    /// </summary>
    /// <example>
    /// Example usage with GetNoise
    /// <code>DomainWarp(ref x, ref y, ref z)
    /// noise = GetNoise(x, y, z)</code>
    /// </example>
    [MethodImpl(OPTIMISE)]
    public void DomainWarp(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        switch (_fractalType)
        {
            default:
                DomainWarpSingle(ref x, ref y, ref z);
                break;
            case FractalType.DomainWarpProgressive:
                DomainWarpFractalProgressive(ref x, ref y, ref z);
                break;
            case FractalType.DomainWarpIndependent:
                DomainWarpFractalIndependent(ref x, ref y, ref z);
                break;
        }
    }

    [MethodImpl(INLINE)]
    private static float FastMin(float a, float b) { return a < b ? a : b; }

    [MethodImpl(INLINE)]
    private static float FastMax(float a, float b) { return a > b ? a : b; }

    [MethodImpl(INLINE)]
    private static float FastAbs(float f) { return f < 0 ? -f : f; }

    [MethodImpl(INLINE)]
    private static float FastSqrt(float f) { return (float)Math.Sqrt(f); }

    [MethodImpl(INLINE)]
    private static int FastFloor(FNLfloat f) { return f >= 0 ? (int)f : (int)f - 1; }

    [MethodImpl(INLINE)]
    private static int FastRound(FNLfloat f) { return f >= 0 ? (int)(f + 0.5f) : (int)(f - 0.5f); }

    [MethodImpl(INLINE)]
    private static float Lerp(float a, float b, float t) { return a + t * (b - a); }

    [MethodImpl(INLINE)]
    private static float InterpHermite(float t) { return t * t * (3 - 2 * t); }

    [MethodImpl(INLINE)]
    private static float InterpQuintic(float t) { return t * t * t * (t * (t * 6 - 15) + 10); }

    [MethodImpl(INLINE)]
    private static float CubicLerp(float a, float b, float c, float d, float t)
    {
        float p = (d - c) - (a - b);
        return t * t * t * p + t * t * ((a - b) - p) + t * (c - a) + b;
    }

    [MethodImpl(INLINE)]
    private static float PingPong(float t)
    {
        t -= (int)(t * 0.5f) * 2;
        return t < 1 ? t : 2 - t;
    }

    private void CalculateFractalBounding()
    {
        float gain = FastAbs(_gain);
        float amp = gain;
        float ampFractal = 1.0f;
        for (int i = 1; i < _octaves; i++)
        {
            ampFractal += amp;
            amp *= gain;
        }
        _fractalBounding = 1 / ampFractal;
    }

    // Hashing
    private const int PrimeX = 501125321;
    private const int PrimeY = 1136930381;
    private const int PrimeZ = 1720413743;

    [MethodImpl(INLINE)]
    private static int Hash(int seed, int xPrimed, int yPrimed)
    {
        int hash = seed ^ xPrimed ^ yPrimed;

        hash *= 0x27d4eb2d;
        return hash;
    }

    [MethodImpl(INLINE)]
    private static int Hash(int seed, int xPrimed, int yPrimed, int zPrimed)
    {
        int hash = seed ^ xPrimed ^ yPrimed ^ zPrimed;

        hash *= 0x27d4eb2d;
        return hash;
    }

    [MethodImpl(INLINE)]
    private static float ValCoord(int seed, int xPrimed, int yPrimed)
    {
        int hash = Hash(seed, xPrimed, yPrimed);

        hash *= hash;
        hash ^= hash << 19;
        return hash * (1 / 2147483648.0f);
    }

    [MethodImpl(INLINE)]
    private static float ValCoord(int seed, int xPrimed, int yPrimed, int zPrimed)
    {
        int hash = Hash(seed, xPrimed, yPrimed, zPrimed);

        hash *= hash;
        hash ^= hash << 19;
        return hash * (1 / 2147483648.0f);
    }

    [MethodImpl(INLINE)]
    private static float GradCoord(int seed, int xPrimed, int yPrimed, float xd, float yd)
    {
        int hash = Hash(seed, xPrimed, yPrimed);
        hash ^= hash >> 15;
        hash &= 127 << 1;

        float xg = FastNoiseLiteTables.Gradients2D[hash];
        float yg = FastNoiseLiteTables.Gradients2D[hash | 1];

        return xd * xg + yd * yg;
    }

    [MethodImpl(INLINE)]
    private static float GradCoord(int seed, int xPrimed, int yPrimed, int zPrimed, float xd, float yd, float zd)
    {
        int hash = Hash(seed, xPrimed, yPrimed, zPrimed);
        hash ^= hash >> 15;
        hash &= 63 << 2;

        float xg = FastNoiseLiteTables.Gradients3D[hash];
        float yg = FastNoiseLiteTables.Gradients3D[hash | 1];
        float zg = FastNoiseLiteTables.Gradients3D[hash | 2];

        return xd * xg + yd * yg + zd * zg;
    }

    [MethodImpl(INLINE)]
    private static void GradCoordOut(int seed, int xPrimed, int yPrimed, out float xo, out float yo)
    {
        int hash = Hash(seed, xPrimed, yPrimed) & (255 << 1);

        xo = FastNoiseLiteTables.RandVecs2D[hash];
        yo = FastNoiseLiteTables.RandVecs2D[hash | 1];
    }

    [MethodImpl(INLINE)]
    private static void GradCoordOut(int seed, int xPrimed, int yPrimed, int zPrimed, out float xo, out float yo, out float zo)
    {
        int hash = Hash(seed, xPrimed, yPrimed, zPrimed) & (255 << 2);

        xo = FastNoiseLiteTables.RandVecs3D[hash];
        yo = FastNoiseLiteTables.RandVecs3D[hash | 1];
        zo = FastNoiseLiteTables.RandVecs3D[hash | 2];
    }

    [MethodImpl(INLINE)]
    private static void GradCoordDual(int seed, int xPrimed, int yPrimed, float xd, float yd, out float xo, out float yo)
    {
        int hash = Hash(seed, xPrimed, yPrimed);
        int index1 = hash & (127 << 1);
        int index2 = (hash >> 7) & (255 << 1);

        float xg = FastNoiseLiteTables.Gradients2D[index1];
        float yg = FastNoiseLiteTables.Gradients2D[index1 | 1];
        float value = xd * xg + yd * yg;

        float xgo = FastNoiseLiteTables.RandVecs2D[index2];
        float ygo = FastNoiseLiteTables.RandVecs2D[index2 | 1];

        xo = value * xgo;
        yo = value * ygo;
    }

    [MethodImpl(INLINE)]
    private static void GradCoordDual(int seed, int xPrimed, int yPrimed, int zPrimed, float xd, float yd, float zd, out float xo, out float yo, out float zo)
    {
        int hash = Hash(seed, xPrimed, yPrimed, zPrimed);
        int index1 = hash & (63 << 2);
        int index2 = (hash >> 6) & (255 << 2);

        float xg = FastNoiseLiteTables.Gradients3D[index1];
        float yg = FastNoiseLiteTables.Gradients3D[index1 | 1];
        float zg = FastNoiseLiteTables.Gradients3D[index1 | 2];
        float value = xd * xg + yd * yg + zd * zg;

        float xgo = FastNoiseLiteTables.RandVecs3D[index2];
        float ygo = FastNoiseLiteTables.RandVecs3D[index2 | 1];
        float zgo = FastNoiseLiteTables.RandVecs3D[index2 | 2];

        xo = value * xgo;
        yo = value * ygo;
        zo = value * zgo;
    }


    // Generic noise gen

    private float GenNoiseSingle(int seed, FNLfloat x, FNLfloat y)
    {
        switch (_noiseType)
        {
            case NoiseType.OpenSimplex2:
                return SingleSimplex(seed, x, y);
            case NoiseType.OpenSimplex2S:
                return SingleOpenSimplex2S(seed, x, y);
            case NoiseType.Cellular:
                return SingleCellular(seed, x, y);
            case NoiseType.Perlin:
                return SinglePerlin(seed, x, y);
            case NoiseType.ValueCubic:
                return SingleValueCubic(seed, x, y);
            case NoiseType.Value:
                return SingleValue(seed, x, y);
            default:
                return 0;
        }
    }

    private float GenNoiseSingle(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        switch (_noiseType)
        {
            case NoiseType.OpenSimplex2:
                return SingleOpenSimplex2(seed, x, y, z);
            case NoiseType.OpenSimplex2S:
                return SingleOpenSimplex2S(seed, x, y, z);
            case NoiseType.Cellular:
                return SingleCellular(seed, x, y, z);
            case NoiseType.Perlin:
                return SinglePerlin(seed, x, y, z);
            case NoiseType.ValueCubic:
                return SingleValueCubic(seed, x, y, z);
            case NoiseType.Value:
                return SingleValue(seed, x, y, z);
            default:
                return 0;
        }
    }


    // Noise Coordinate Transforms (frequency, and possible skew or rotation)

    [MethodImpl(INLINE)]
    private void TransformNoiseCoordinate(ref FNLfloat x, ref FNLfloat y)
    {
        x *= _frequency;
        y *= _frequency;

        switch (_noiseType)
        {
            case NoiseType.OpenSimplex2:
            case NoiseType.OpenSimplex2S:
                {
                    const FNLfloat SQRT3 = (FNLfloat)1.7320508075688772935274463415059;
                    const FNLfloat F2 = 0.5f * (SQRT3 - 1);
                    FNLfloat t = (x + y) * F2;
                    x += t;
                    y += t;
                }
                break;
            default:
                break;
        }
    }

    [MethodImpl(INLINE)]
    private void TransformNoiseCoordinate(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        x *= _frequency;
        y *= _frequency;
        z *= _frequency;

        switch (_transformType3D)
        {
            case TransformType3D.ImproveXYPlanes:
                {
                    FNLfloat xy = x + y;
                    FNLfloat s2 = xy * -(FNLfloat)0.211324865405187;
                    z *= (FNLfloat)0.577350269189626;
                    x += s2 - z;
                    y = y + s2 - z;
                    z += xy * (FNLfloat)0.577350269189626;
                }
                break;
            case TransformType3D.ImproveXZPlanes:
                {
                    FNLfloat xz = x + z;
                    FNLfloat s2 = xz * -(FNLfloat)0.211324865405187;
                    y *= (FNLfloat)0.577350269189626;
                    x += s2 - y;
                    z += s2 - y;
                    y += xz * (FNLfloat)0.577350269189626;
                }
                break;
            case TransformType3D.DefaultOpenSimplex2:
                {
                    const FNLfloat R3 = (FNLfloat)(2.0 / 3.0);
                    FNLfloat r = (x + y + z) * R3; // Rotation, not skew
                    x = r - x;
                    y = r - y;
                    z = r - z;
                }
                break;
            default:
                break;
        }
    }

    private void UpdateTransformType3D()
    {
        switch (_rotationType3D)
        {
            case RotationType3D.ImproveXYPlanes:
                _transformType3D = TransformType3D.ImproveXYPlanes;
                break;
            case RotationType3D.ImproveXZPlanes:
                _transformType3D = TransformType3D.ImproveXZPlanes;
                break;
            default:
                switch (_noiseType)
                {
                    case NoiseType.OpenSimplex2:
                    case NoiseType.OpenSimplex2S:
                        _transformType3D = TransformType3D.DefaultOpenSimplex2;
                        break;
                    default:
                        _transformType3D = TransformType3D.None;
                        break;
                }
                break;
        }
    }


    // Domain Warp Coordinate Transforms

    [MethodImpl(INLINE)]
    private void TransformDomainWarpCoordinate(ref FNLfloat x, ref FNLfloat y)
    {
        switch (_domainWarpType)
        {
            case DomainWarpType.OpenSimplex2:
            case DomainWarpType.OpenSimplex2Reduced:
                {
                    const FNLfloat SQRT3 = (FNLfloat)1.7320508075688772935274463415059;
                    const FNLfloat F2 = 0.5f * (SQRT3 - 1);
                    FNLfloat t = (x + y) * F2;
                    x += t; y += t;
                }
                break;
            default:
                break;
        }
    }

    [MethodImpl(INLINE)]
    private void TransformDomainWarpCoordinate(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        switch (_warpTransformType3D)
        {
            case TransformType3D.ImproveXYPlanes:
                {
                    FNLfloat xy = x + y;
                    FNLfloat s2 = xy * -(FNLfloat)0.211324865405187;
                    z *= (FNLfloat)0.577350269189626;
                    x += s2 - z;
                    y = y + s2 - z;
                    z += xy * (FNLfloat)0.577350269189626;
                }
                break;
            case TransformType3D.ImproveXZPlanes:
                {
                    FNLfloat xz = x + z;
                    FNLfloat s2 = xz * -(FNLfloat)0.211324865405187;
                    y *= (FNLfloat)0.577350269189626;
                    x += s2 - y; z += s2 - y;
                    y += xz * (FNLfloat)0.577350269189626;
                }
                break;
            case TransformType3D.DefaultOpenSimplex2:
                {
                    const FNLfloat R3 = (FNLfloat)(2.0 / 3.0);
                    FNLfloat r = (x + y + z) * R3; // Rotation, not skew
                    x = r - x;
                    y = r - y;
                    z = r - z;
                }
                break;
            default:
                break;
        }
    }

    private void UpdateWarpTransformType3D()
    {
        switch (_rotationType3D)
        {
            case RotationType3D.ImproveXYPlanes:
                _warpTransformType3D = TransformType3D.ImproveXYPlanes;
                break;
            case RotationType3D.ImproveXZPlanes:
                _warpTransformType3D = TransformType3D.ImproveXZPlanes;
                break;
            default:
                switch (_domainWarpType)
                {
                    case DomainWarpType.OpenSimplex2:
                    case DomainWarpType.OpenSimplex2Reduced:
                        _warpTransformType3D = TransformType3D.DefaultOpenSimplex2;
                        break;
                    default:
                        _warpTransformType3D = TransformType3D.None;
                        break;
                }
                break;
        }
    }


    // Fractal FBm

    private float GenFractalFBm(FNLfloat x, FNLfloat y)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = GenNoiseSingle(seed++, x, y);
            sum += noise * amp;
            amp *= Lerp(1.0f, FastMin(noise + 1, 2) * 0.5f, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }

    private float GenFractalFBm(FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = GenNoiseSingle(seed++, x, y, z);
            sum += noise * amp;
            amp *= Lerp(1.0f, (noise + 1) * 0.5f, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            z *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }


    // Fractal Ridged

    private float GenFractalRidged(FNLfloat x, FNLfloat y)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = FastAbs(GenNoiseSingle(seed++, x, y));
            sum += (noise * -2 + 1) * amp;
            amp *= Lerp(1.0f, 1 - noise, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }

    private float GenFractalRidged(FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = FastAbs(GenNoiseSingle(seed++, x, y, z));
            sum += (noise * -2 + 1) * amp;
            amp *= Lerp(1.0f, 1 - noise, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            z *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }


    // Fractal PingPong 

    private float GenFractalPingPong(FNLfloat x, FNLfloat y)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = PingPong((GenNoiseSingle(seed++, x, y) + 1) * _pingPongStrength);
            sum += (noise - 0.5f) * 2 * amp;
            amp *= Lerp(1.0f, noise, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }

    private float GenFractalPingPong(FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int seed = _seed;
        float sum = 0;
        float amp = _fractalBounding;

        for (int i = 0; i < _octaves; i++)
        {
            float noise = PingPong((GenNoiseSingle(seed++, x, y, z) + 1) * _pingPongStrength);
            sum += (noise - 0.5f) * 2 * amp;
            amp *= Lerp(1.0f, noise, _weightedStrength);

            x *= _lacunarity;
            y *= _lacunarity;
            z *= _lacunarity;
            amp *= _gain;
        }

        return sum;
    }


    // Simplex/OpenSimplex2 Noise

    private float SingleSimplex(int seed, FNLfloat x, FNLfloat y)
    {
        // 2D OpenSimplex2 case uses the same algorithm as ordinary Simplex.

        const float SQRT3 = 1.7320508075688772935274463415059f;
        const float G2 = (3 - SQRT3) / 6;

        /*
         * --- Skew moved to TransformNoiseCoordinate method ---
         * const FNfloat F2 = 0.5f * (SQRT3 - 1);
         * FNfloat s = (x + y) * F2;
         * x += s; y += s;
        */

        int i = FastFloor(x);
        int j = FastFloor(y);
        float xi = (float)(x - i);
        float yi = (float)(y - j);

        float t = (xi + yi) * G2;
        float x0 = (float)(xi - t);
        float y0 = (float)(yi - t);

        i *= PrimeX;
        j *= PrimeY;

        float n0, n1, n2;

        float a = 0.5f - x0 * x0 - y0 * y0;
        if (a <= 0) n0 = 0;
        else
        {
            n0 = (a * a) * (a * a) * GradCoord(seed, i, j, x0, y0);
        }

        float c = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a);
        if (c <= 0) n2 = 0;
        else
        {
            float x2 = x0 + (2 * (float)G2 - 1);
            float y2 = y0 + (2 * (float)G2 - 1);
            n2 = (c * c) * (c * c) * GradCoord(seed, i + PrimeX, j + PrimeY, x2, y2);
        }

        if (y0 > x0)
        {
            float x1 = x0 + (float)G2;
            float y1 = y0 + ((float)G2 - 1);
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b <= 0) n1 = 0;
            else
            {
                n1 = (b * b) * (b * b) * GradCoord(seed, i, j + PrimeY, x1, y1);
            }
        }
        else
        {
            float x1 = x0 + ((float)G2 - 1);
            float y1 = y0 + (float)G2;
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b <= 0) n1 = 0;
            else
            {
                n1 = (b * b) * (b * b) * GradCoord(seed, i + PrimeX, j, x1, y1);
            }
        }

        return (n0 + n1 + n2) * 99.83685446303647f;
    }

    private float SingleOpenSimplex2(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        // 3D OpenSimplex2 case uses two offset rotated cube grids.

        /*
         * --- Rotation moved to TransformNoiseCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = FastRound(x);
        int j = FastRound(y);
        int k = FastRound(z);
        float x0 = (float)(x - i);
        float y0 = (float)(y - j);
        float z0 = (float)(z - k);

        int xNSign = (int)(-1.0f - x0) | 1;
        int yNSign = (int)(-1.0f - y0) | 1;
        int zNSign = (int)(-1.0f - z0) | 1;

        float ax0 = xNSign * -x0;
        float ay0 = yNSign * -y0;
        float az0 = zNSign * -z0;

        i *= PrimeX;
        j *= PrimeY;
        k *= PrimeZ;

        float value = 0;
        float a = (0.6f - x0 * x0) - (y0 * y0 + z0 * z0);

        for (int l = 0; ; l++)
        {
            if (a > 0)
            {
                value += (a * a) * (a * a) * GradCoord(seed, i, j, k, x0, y0, z0);
            }

            if (ax0 >= ay0 && ax0 >= az0)
            {
                float b = a + ax0 + ax0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * GradCoord(seed, i - xNSign * PrimeX, j, k, x0 + xNSign, y0, z0);
                }
            }
            else if (ay0 > ax0 && ay0 >= az0)
            {
                float b = a + ay0 + ay0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * GradCoord(seed, i, j - yNSign * PrimeY, k, x0, y0 + yNSign, z0);
                }
            }
            else
            {
                float b = a + az0 + az0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * GradCoord(seed, i, j, k - zNSign * PrimeZ, x0, y0, z0 + zNSign);
                }
            }

            if (l == 1) break;

            ax0 = 0.5f - ax0;
            ay0 = 0.5f - ay0;
            az0 = 0.5f - az0;

            x0 = xNSign * ax0;
            y0 = yNSign * ay0;
            z0 = zNSign * az0;

            a += (0.75f - ax0) - (ay0 + az0);

            i += (xNSign >> 1) & PrimeX;
            j += (yNSign >> 1) & PrimeY;
            k += (zNSign >> 1) & PrimeZ;

            xNSign = -xNSign;
            yNSign = -yNSign;
            zNSign = -zNSign;

            seed = ~seed;
        }

        return value * 32.69428253173828125f;
    }


    // OpenSimplex2S Noise

    private float SingleOpenSimplex2S(int seed, FNLfloat x, FNLfloat y)
    {
        // 2D OpenSimplex2S case is a modified 2D simplex noise.

        const FNLfloat SQRT3 = (FNLfloat)1.7320508075688772935274463415059;
        const FNLfloat G2 = (3 - SQRT3) / 6;

        /*
         * --- Skew moved to TransformNoiseCoordinate method ---
         * const FNfloat F2 = 0.5f * (SQRT3 - 1);
         * FNfloat s = (x + y) * F2;
         * x += s; y += s;
        */

        int i = FastFloor(x);
        int j = FastFloor(y);
        float xi = (float)(x - i);
        float yi = (float)(y - j);

        i *= PrimeX;
        j *= PrimeY;
        int i1 = i + PrimeX;
        int j1 = j + PrimeY;

        float t = (xi + yi) * (float)G2;
        float x0 = xi - t;
        float y0 = yi - t;

        float a0 = (2.0f / 3.0f) - x0 * x0 - y0 * y0;
        float value = (a0 * a0) * (a0 * a0) * GradCoord(seed, i, j, x0, y0);

        float a1 = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a0);
        float x1 = x0 - (float)(1 - 2 * G2);
        float y1 = y0 - (float)(1 - 2 * G2);
        value += (a1 * a1) * (a1 * a1) * GradCoord(seed, i1, j1, x1, y1);

        // Nested conditionals were faster than compact bit logic/arithmetic.
        float xmyi = xi - yi;
        if (t > G2)
        {
            if (xi + xmyi > 1)
            {
                float x2 = x0 + (float)(3 * G2 - 2);
                float y2 = y0 + (float)(3 * G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i + (PrimeX << 1), j + PrimeY, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)G2;
                float y2 = y0 + (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i, j + PrimeY, x2, y2);
                }
            }

            if (yi - xmyi > 1)
            {
                float x3 = x0 + (float)(3 * G2 - 1);
                float y3 = y0 + (float)(3 * G2 - 2);
                float a3 = (2.0f / 3.0f) - x3 * x3 - y3 * y3;
                if (a3 > 0)
                {
                    value += (a3 * a3) * (a3 * a3) * GradCoord(seed, i + PrimeX, j + (PrimeY << 1), x3, y3);
                }
            }
            else
            {
                float x3 = x0 + (float)(G2 - 1);
                float y3 = y0 + (float)G2;
                float a3 = (2.0f / 3.0f) - x3 * x3 - y3 * y3;
                if (a3 > 0)
                {
                    value += (a3 * a3) * (a3 * a3) * GradCoord(seed, i + PrimeX, j, x3, y3);
                }
            }
        }
        else
        {
            if (xi + xmyi < 0)
            {
                float x2 = x0 + (float)(1 - G2);
                float y2 = y0 - (float)G2;
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i - PrimeX, j, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)(G2 - 1);
                float y2 = y0 + (float)G2;
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i + PrimeX, j, x2, y2);
                }
            }

            if (yi < xmyi)
            {
                float x2 = x0 - (float)G2;
                float y2 = y0 - (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i, j - PrimeY, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)G2;
                float y2 = y0 + (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * GradCoord(seed, i, j + PrimeY, x2, y2);
                }
            }
        }

        return value * 18.24196194486065f;
    }

    private float SingleOpenSimplex2S(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        // 3D OpenSimplex2S case uses two offset rotated cube grids.

        /*
         * --- Rotation moved to TransformNoiseCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = FastFloor(x);
        int j = FastFloor(y);
        int k = FastFloor(z);
        float xi = (float)(x - i);
        float yi = (float)(y - j);
        float zi = (float)(z - k);

        i *= PrimeX;
        j *= PrimeY;
        k *= PrimeZ;
        int seed2 = seed + 1293373;

        int xNMask = (int)(-0.5f - xi);
        int yNMask = (int)(-0.5f - yi);
        int zNMask = (int)(-0.5f - zi);

        float x0 = xi + xNMask;
        float y0 = yi + yNMask;
        float z0 = zi + zNMask;
        float a0 = 0.75f - x0 * x0 - y0 * y0 - z0 * z0;
        float value = (a0 * a0) * (a0 * a0) * GradCoord(seed,
            i + (xNMask & PrimeX), j + (yNMask & PrimeY), k + (zNMask & PrimeZ), x0, y0, z0);

        float x1 = xi - 0.5f;
        float y1 = yi - 0.5f;
        float z1 = zi - 0.5f;
        float a1 = 0.75f - x1 * x1 - y1 * y1 - z1 * z1;
        value += (a1 * a1) * (a1 * a1) * GradCoord(seed2,
            i + PrimeX, j + PrimeY, k + PrimeZ, x1, y1, z1);

        float xAFlipMask0 = ((xNMask | 1) << 1) * x1;
        float yAFlipMask0 = ((yNMask | 1) << 1) * y1;
        float zAFlipMask0 = ((zNMask | 1) << 1) * z1;
        float xAFlipMask1 = (-2 - (xNMask << 2)) * x1 - 1.0f;
        float yAFlipMask1 = (-2 - (yNMask << 2)) * y1 - 1.0f;
        float zAFlipMask1 = (-2 - (zNMask << 2)) * z1 - 1.0f;

        bool skip5 = false;
        float a2 = xAFlipMask0 + a0;
        if (a2 > 0)
        {
            float x2 = x0 - (xNMask | 1);
            float y2 = y0;
            float z2 = z0;
            value += (a2 * a2) * (a2 * a2) * GradCoord(seed,
                i + (~xNMask & PrimeX), j + (yNMask & PrimeY), k + (zNMask & PrimeZ), x2, y2, z2);
        }
        else
        {
            float a3 = yAFlipMask0 + zAFlipMask0 + a0;
            if (a3 > 0)
            {
                float x3 = x0;
                float y3 = y0 - (yNMask | 1);
                float z3 = z0 - (zNMask | 1);
                value += (a3 * a3) * (a3 * a3) * GradCoord(seed,
                    i + (xNMask & PrimeX), j + (~yNMask & PrimeY), k + (~zNMask & PrimeZ), x3, y3, z3);
            }

            float a4 = xAFlipMask1 + a1;
            if (a4 > 0)
            {
                float x4 = (xNMask | 1) + x1;
                float y4 = y1;
                float z4 = z1;
                value += (a4 * a4) * (a4 * a4) * GradCoord(seed2,
                    i + (xNMask & (PrimeX * 2)), j + PrimeY, k + PrimeZ, x4, y4, z4);
                skip5 = true;
            }
        }

        bool skip9 = false;
        float a6 = yAFlipMask0 + a0;
        if (a6 > 0)
        {
            float x6 = x0;
            float y6 = y0 - (yNMask | 1);
            float z6 = z0;
            value += (a6 * a6) * (a6 * a6) * GradCoord(seed,
                i + (xNMask & PrimeX), j + (~yNMask & PrimeY), k + (zNMask & PrimeZ), x6, y6, z6);
        }
        else
        {
            float a7 = xAFlipMask0 + zAFlipMask0 + a0;
            if (a7 > 0)
            {
                float x7 = x0 - (xNMask | 1);
                float y7 = y0;
                float z7 = z0 - (zNMask | 1);
                value += (a7 * a7) * (a7 * a7) * GradCoord(seed,
                    i + (~xNMask & PrimeX), j + (yNMask & PrimeY), k + (~zNMask & PrimeZ), x7, y7, z7);
            }

            float a8 = yAFlipMask1 + a1;
            if (a8 > 0)
            {
                float x8 = x1;
                float y8 = (yNMask | 1) + y1;
                float z8 = z1;
                value += (a8 * a8) * (a8 * a8) * GradCoord(seed2,
                    i + PrimeX, j + (yNMask & (PrimeY << 1)), k + PrimeZ, x8, y8, z8);
                skip9 = true;
            }
        }

        bool skipD = false;
        float aA = zAFlipMask0 + a0;
        if (aA > 0)
        {
            float xA = x0;
            float yA = y0;
            float zA = z0 - (zNMask | 1);
            value += (aA * aA) * (aA * aA) * GradCoord(seed,
                i + (xNMask & PrimeX), j + (yNMask & PrimeY), k + (~zNMask & PrimeZ), xA, yA, zA);
        }
        else
        {
            float aB = xAFlipMask0 + yAFlipMask0 + a0;
            if (aB > 0)
            {
                float xB = x0 - (xNMask | 1);
                float yB = y0 - (yNMask | 1);
                float zB = z0;
                value += (aB * aB) * (aB * aB) * GradCoord(seed,
                    i + (~xNMask & PrimeX), j + (~yNMask & PrimeY), k + (zNMask & PrimeZ), xB, yB, zB);
            }

            float aC = zAFlipMask1 + a1;
            if (aC > 0)
            {
                float xC = x1;
                float yC = y1;
                float zC = (zNMask | 1) + z1;
                value += (aC * aC) * (aC * aC) * GradCoord(seed2,
                    i + PrimeX, j + PrimeY, k + (zNMask & (PrimeZ << 1)), xC, yC, zC);
                skipD = true;
            }
        }

        if (!skip5)
        {
            float a5 = yAFlipMask1 + zAFlipMask1 + a1;
            if (a5 > 0)
            {
                float x5 = x1;
                float y5 = (yNMask | 1) + y1;
                float z5 = (zNMask | 1) + z1;
                value += (a5 * a5) * (a5 * a5) * GradCoord(seed2,
                    i + PrimeX, j + (yNMask & (PrimeY << 1)), k + (zNMask & (PrimeZ << 1)), x5, y5, z5);
            }
        }

        if (!skip9)
        {
            float a9 = xAFlipMask1 + zAFlipMask1 + a1;
            if (a9 > 0)
            {
                float x9 = (xNMask | 1) + x1;
                float y9 = y1;
                float z9 = (zNMask | 1) + z1;
                value += (a9 * a9) * (a9 * a9) * GradCoord(seed2,
                    i + (xNMask & (PrimeX * 2)), j + PrimeY, k + (zNMask & (PrimeZ << 1)), x9, y9, z9);
            }
        }

        if (!skipD)
        {
            float aD = xAFlipMask1 + yAFlipMask1 + a1;
            if (aD > 0)
            {
                float xD = (xNMask | 1) + x1;
                float yD = (yNMask | 1) + y1;
                float zD = z1;
                value += (aD * aD) * (aD * aD) * GradCoord(seed2,
                    i + (xNMask & (PrimeX << 1)), j + (yNMask & (PrimeY << 1)), k + PrimeZ, xD, yD, zD);
            }
        }

        return value * 9.046026385208288f;
    }


    // Cellular Noise

    private float SingleCellular(int seed, FNLfloat x, FNLfloat y)
    {
        int xr = FastRound(x);
        int yr = FastRound(y);

        float distance0 = float.MaxValue;
        float distance1 = float.MaxValue;
        int closestHash = 0;

        float cellularJitter = 0.43701595f * _cellularJitterModifier;

        int xPrimed = (xr - 1) * PrimeX;
        int yPrimedBase = (yr - 1) * PrimeY;

        switch (_cellularDistanceFunction)
        {
            default:
            case CellularDistanceFunction.Euclidean:
            case CellularDistanceFunction.EuclideanSq:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = vecX * vecX + vecY * vecY;

                        distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
            case CellularDistanceFunction.Manhattan:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = FastAbs(vecX) + FastAbs(vecY);

                        distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
            case CellularDistanceFunction.Hybrid:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = (FastAbs(vecX) + FastAbs(vecY)) + (vecX * vecX + vecY * vecY);

                        distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
        }

        if (_cellularDistanceFunction == CellularDistanceFunction.Euclidean && _cellularReturnType >= CellularReturnType.Distance)
        {
            distance0 = FastSqrt(distance0);

            if (_cellularReturnType >= CellularReturnType.Distance2)
            {
                distance1 = FastSqrt(distance1);
            }
        }

        switch (_cellularReturnType)
        {
            case CellularReturnType.CellValue:
                return closestHash * (1 / 2147483648.0f);
            case CellularReturnType.Distance:
                return distance0 - 1;
            case CellularReturnType.Distance2:
                return distance1 - 1;
            case CellularReturnType.Distance2Add:
                return (distance1 + distance0) * 0.5f - 1;
            case CellularReturnType.Distance2Sub:
                return distance1 - distance0 - 1;
            case CellularReturnType.Distance2Mul:
                return distance1 * distance0 * 0.5f - 1;
            case CellularReturnType.Distance2Div:
                return distance0 / distance1 - 1;
            default:
                return 0;
        }
    }

    private float SingleCellular(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int xr = FastRound(x);
        int yr = FastRound(y);
        int zr = FastRound(z);

        float distance0 = float.MaxValue;
        float distance1 = float.MaxValue;
        int closestHash = 0;

        float cellularJitter = 0.39614353f * _cellularJitterModifier;

        int xPrimed = (xr - 1) * PrimeX;
        int yPrimedBase = (yr - 1) * PrimeY;
        int zPrimedBase = (zr - 1) * PrimeZ;

        switch (_cellularDistanceFunction)
        {
            case CellularDistanceFunction.Euclidean:
            case CellularDistanceFunction.EuclideanSq:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + FastNoiseLiteTables.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = vecX * vecX + vecY * vecY + vecZ * vecZ;

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += PrimeZ;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
            case CellularDistanceFunction.Manhattan:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + FastNoiseLiteTables.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = FastAbs(vecX) + FastAbs(vecY) + FastAbs(vecZ);

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += PrimeZ;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
            case CellularDistanceFunction.Hybrid:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + FastNoiseLiteTables.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + FastNoiseLiteTables.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + FastNoiseLiteTables.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = (FastAbs(vecX) + FastAbs(vecY) + FastAbs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);

                            distance1 = FastMax(FastMin(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += PrimeZ;
                        }
                        yPrimed += PrimeY;
                    }
                    xPrimed += PrimeX;
                }
                break;
            default:
                break;
        }

        if (_cellularDistanceFunction == CellularDistanceFunction.Euclidean && _cellularReturnType >= CellularReturnType.Distance)
        {
            distance0 = FastSqrt(distance0);

            if (_cellularReturnType >= CellularReturnType.Distance2)
            {
                distance1 = FastSqrt(distance1);
            }
        }

        switch (_cellularReturnType)
        {
            case CellularReturnType.CellValue:
                return closestHash * (1 / 2147483648.0f);
            case CellularReturnType.Distance:
                return distance0 - 1;
            case CellularReturnType.Distance2:
                return distance1 - 1;
            case CellularReturnType.Distance2Add:
                return (distance1 + distance0) * 0.5f - 1;
            case CellularReturnType.Distance2Sub:
                return distance1 - distance0 - 1;
            case CellularReturnType.Distance2Mul:
                return distance1 * distance0 * 0.5f - 1;
            case CellularReturnType.Distance2Div:
                return distance0 / distance1 - 1;
            default:
                return 0;
        }
    }


    // Perlin Noise

    private float SinglePerlin(int seed, FNLfloat x, FNLfloat y)
    {
        int x0 = FastFloor(x);
        int y0 = FastFloor(y);

        float xd0 = (float)(x - x0);
        float yd0 = (float)(y - y0);
        float xd1 = xd0 - 1;
        float yd1 = yd0 - 1;

        float xs = InterpQuintic(xd0);
        float ys = InterpQuintic(yd0);

        x0 *= PrimeX;
        y0 *= PrimeY;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;

        float xf0 = Lerp(GradCoord(seed, x0, y0, xd0, yd0), GradCoord(seed, x1, y0, xd1, yd0), xs);
        float xf1 = Lerp(GradCoord(seed, x0, y1, xd0, yd1), GradCoord(seed, x1, y1, xd1, yd1), xs);

        return Lerp(xf0, xf1, ys) * 1.4247691104677813f;
    }

    private float SinglePerlin(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int x0 = FastFloor(x);
        int y0 = FastFloor(y);
        int z0 = FastFloor(z);

        float xd0 = (float)(x - x0);
        float yd0 = (float)(y - y0);
        float zd0 = (float)(z - z0);
        float xd1 = xd0 - 1;
        float yd1 = yd0 - 1;
        float zd1 = zd0 - 1;

        float xs = InterpQuintic(xd0);
        float ys = InterpQuintic(yd0);
        float zs = InterpQuintic(zd0);

        x0 *= PrimeX;
        y0 *= PrimeY;
        z0 *= PrimeZ;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;
        int z1 = z0 + PrimeZ;

        float xf00 = Lerp(GradCoord(seed, x0, y0, z0, xd0, yd0, zd0), GradCoord(seed, x1, y0, z0, xd1, yd0, zd0), xs);
        float xf10 = Lerp(GradCoord(seed, x0, y1, z0, xd0, yd1, zd0), GradCoord(seed, x1, y1, z0, xd1, yd1, zd0), xs);
        float xf01 = Lerp(GradCoord(seed, x0, y0, z1, xd0, yd0, zd1), GradCoord(seed, x1, y0, z1, xd1, yd0, zd1), xs);
        float xf11 = Lerp(GradCoord(seed, x0, y1, z1, xd0, yd1, zd1), GradCoord(seed, x1, y1, z1, xd1, yd1, zd1), xs);

        float yf0 = Lerp(xf00, xf10, ys);
        float yf1 = Lerp(xf01, xf11, ys);

        return Lerp(yf0, yf1, zs) * 0.964921414852142333984375f;
    }


    // Value Cubic Noise

    private float SingleValueCubic(int seed, FNLfloat x, FNLfloat y)
    {
        int x1 = FastFloor(x);
        int y1 = FastFloor(y);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);

        x1 *= PrimeX;
        y1 *= PrimeY;
        int x0 = x1 - PrimeX;
        int y0 = y1 - PrimeY;
        int x2 = x1 + PrimeX;
        int y2 = y1 + PrimeY;
        int x3 = x1 + unchecked(PrimeX * 2);
        int y3 = y1 + unchecked(PrimeY * 2);

        return CubicLerp(
            CubicLerp(ValCoord(seed, x0, y0), ValCoord(seed, x1, y0), ValCoord(seed, x2, y0), ValCoord(seed, x3, y0),
            xs),
            CubicLerp(ValCoord(seed, x0, y1), ValCoord(seed, x1, y1), ValCoord(seed, x2, y1), ValCoord(seed, x3, y1),
            xs),
            CubicLerp(ValCoord(seed, x0, y2), ValCoord(seed, x1, y2), ValCoord(seed, x2, y2), ValCoord(seed, x3, y2),
            xs),
            CubicLerp(ValCoord(seed, x0, y3), ValCoord(seed, x1, y3), ValCoord(seed, x2, y3), ValCoord(seed, x3, y3),
            xs),
            ys) * (1 / (1.5f * 1.5f));
    }

    private float SingleValueCubic(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int x1 = FastFloor(x);
        int y1 = FastFloor(y);
        int z1 = FastFloor(z);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);
        float zs = (float)(z - z1);

        x1 *= PrimeX;
        y1 *= PrimeY;
        z1 *= PrimeZ;

        int x0 = x1 - PrimeX;
        int y0 = y1 - PrimeY;
        int z0 = z1 - PrimeZ;
        int x2 = x1 + PrimeX;
        int y2 = y1 + PrimeY;
        int z2 = z1 + PrimeZ;
        int x3 = x1 + unchecked(PrimeX * 2);
        int y3 = y1 + unchecked(PrimeY * 2);
        int z3 = z1 + unchecked(PrimeZ * 2);


        return CubicLerp(
            CubicLerp(
            CubicLerp(ValCoord(seed, x0, y0, z0), ValCoord(seed, x1, y0, z0), ValCoord(seed, x2, y0, z0), ValCoord(seed, x3, y0, z0), xs),
            CubicLerp(ValCoord(seed, x0, y1, z0), ValCoord(seed, x1, y1, z0), ValCoord(seed, x2, y1, z0), ValCoord(seed, x3, y1, z0), xs),
            CubicLerp(ValCoord(seed, x0, y2, z0), ValCoord(seed, x1, y2, z0), ValCoord(seed, x2, y2, z0), ValCoord(seed, x3, y2, z0), xs),
            CubicLerp(ValCoord(seed, x0, y3, z0), ValCoord(seed, x1, y3, z0), ValCoord(seed, x2, y3, z0), ValCoord(seed, x3, y3, z0), xs),
            ys),
            CubicLerp(
            CubicLerp(ValCoord(seed, x0, y0, z1), ValCoord(seed, x1, y0, z1), ValCoord(seed, x2, y0, z1), ValCoord(seed, x3, y0, z1), xs),
            CubicLerp(ValCoord(seed, x0, y1, z1), ValCoord(seed, x1, y1, z1), ValCoord(seed, x2, y1, z1), ValCoord(seed, x3, y1, z1), xs),
            CubicLerp(ValCoord(seed, x0, y2, z1), ValCoord(seed, x1, y2, z1), ValCoord(seed, x2, y2, z1), ValCoord(seed, x3, y2, z1), xs),
            CubicLerp(ValCoord(seed, x0, y3, z1), ValCoord(seed, x1, y3, z1), ValCoord(seed, x2, y3, z1), ValCoord(seed, x3, y3, z1), xs),
            ys),
            CubicLerp(
            CubicLerp(ValCoord(seed, x0, y0, z2), ValCoord(seed, x1, y0, z2), ValCoord(seed, x2, y0, z2), ValCoord(seed, x3, y0, z2), xs),
            CubicLerp(ValCoord(seed, x0, y1, z2), ValCoord(seed, x1, y1, z2), ValCoord(seed, x2, y1, z2), ValCoord(seed, x3, y1, z2), xs),
            CubicLerp(ValCoord(seed, x0, y2, z2), ValCoord(seed, x1, y2, z2), ValCoord(seed, x2, y2, z2), ValCoord(seed, x3, y2, z2), xs),
            CubicLerp(ValCoord(seed, x0, y3, z2), ValCoord(seed, x1, y3, z2), ValCoord(seed, x2, y3, z2), ValCoord(seed, x3, y3, z2), xs),
            ys),
            CubicLerp(
            CubicLerp(ValCoord(seed, x0, y0, z3), ValCoord(seed, x1, y0, z3), ValCoord(seed, x2, y0, z3), ValCoord(seed, x3, y0, z3), xs),
            CubicLerp(ValCoord(seed, x0, y1, z3), ValCoord(seed, x1, y1, z3), ValCoord(seed, x2, y1, z3), ValCoord(seed, x3, y1, z3), xs),
            CubicLerp(ValCoord(seed, x0, y2, z3), ValCoord(seed, x1, y2, z3), ValCoord(seed, x2, y2, z3), ValCoord(seed, x3, y2, z3), xs),
            CubicLerp(ValCoord(seed, x0, y3, z3), ValCoord(seed, x1, y3, z3), ValCoord(seed, x2, y3, z3), ValCoord(seed, x3, y3, z3), xs),
            ys),
            zs) * (1 / (1.5f * 1.5f * 1.5f));
    }


    // Value Noise

    private float SingleValue(int seed, FNLfloat x, FNLfloat y)
    {
        int x0 = FastFloor(x);
        int y0 = FastFloor(y);

        float xs = InterpHermite((float)(x - x0));
        float ys = InterpHermite((float)(y - y0));

        x0 *= PrimeX;
        y0 *= PrimeY;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;

        float xf0 = Lerp(ValCoord(seed, x0, y0), ValCoord(seed, x1, y0), xs);
        float xf1 = Lerp(ValCoord(seed, x0, y1), ValCoord(seed, x1, y1), xs);

        return Lerp(xf0, xf1, ys);
    }

    private float SingleValue(int seed, FNLfloat x, FNLfloat y, FNLfloat z)
    {
        int x0 = FastFloor(x);
        int y0 = FastFloor(y);
        int z0 = FastFloor(z);

        float xs = InterpHermite((float)(x - x0));
        float ys = InterpHermite((float)(y - y0));
        float zs = InterpHermite((float)(z - z0));

        x0 *= PrimeX;
        y0 *= PrimeY;
        z0 *= PrimeZ;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;
        int z1 = z0 + PrimeZ;

        float xf00 = Lerp(ValCoord(seed, x0, y0, z0), ValCoord(seed, x1, y0, z0), xs);
        float xf10 = Lerp(ValCoord(seed, x0, y1, z0), ValCoord(seed, x1, y1, z0), xs);
        float xf01 = Lerp(ValCoord(seed, x0, y0, z1), ValCoord(seed, x1, y0, z1), xs);
        float xf11 = Lerp(ValCoord(seed, x0, y1, z1), ValCoord(seed, x1, y1, z1), xs);

        float yf0 = Lerp(xf00, xf10, ys);
        float yf1 = Lerp(xf01, xf11, ys);

        return Lerp(yf0, yf1, zs);
    }


    // Domain Warp

    private void DoSingleDomainWarp(int seed, float amp, float freq, FNLfloat x, FNLfloat y, ref FNLfloat xr, ref FNLfloat yr)
    {
        switch (_domainWarpType)
        {
            case DomainWarpType.OpenSimplex2:
                SingleDomainWarpSimplexGradient(seed, amp * 38.283687591552734375f, freq, x, y, ref xr, ref yr, false);
                break;
            case DomainWarpType.OpenSimplex2Reduced:
                SingleDomainWarpSimplexGradient(seed, amp * 16.0f, freq, x, y, ref xr, ref yr, true);
                break;
            case DomainWarpType.BasicGrid:
                SingleDomainWarpBasicGrid(seed, amp, freq, x, y, ref xr, ref yr);
                break;
        }
    }

    private void DoSingleDomainWarp(int seed, float amp, float freq, FNLfloat x, FNLfloat y, FNLfloat z, ref FNLfloat xr, ref FNLfloat yr, ref FNLfloat zr)
    {
        switch (_domainWarpType)
        {
            case DomainWarpType.OpenSimplex2:
                SingleDomainWarpOpenSimplex2Gradient(seed, amp * 32.69428253173828125f, freq, x, y, z, ref xr, ref yr, ref zr, false);
                break;
            case DomainWarpType.OpenSimplex2Reduced:
                SingleDomainWarpOpenSimplex2Gradient(seed, amp * 7.71604938271605f, freq, x, y, z, ref xr, ref yr, ref zr, true);
                break;
            case DomainWarpType.BasicGrid:
                SingleDomainWarpBasicGrid(seed, amp, freq, x, y, z, ref xr, ref yr, ref zr);
                break;
        }
    }


    // Domain Warp Single Wrapper

    private void DomainWarpSingle(ref FNLfloat x, ref FNLfloat y)
    {
        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        FNLfloat xs = x;
        FNLfloat ys = y;
        TransformDomainWarpCoordinate(ref xs, ref ys);

        DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);
    }

    private void DomainWarpSingle(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        FNLfloat xs = x;
        FNLfloat ys = y;
        FNLfloat zs = z;
        TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

        DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);
    }


    // Domain Warp Fractal Progressive

    private void DomainWarpFractalProgressive(ref FNLfloat x, ref FNLfloat y)
    {
        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        for (int i = 0; i < _octaves; i++)
        {
            FNLfloat xs = x;
            FNLfloat ys = y;
            TransformDomainWarpCoordinate(ref xs, ref ys);

            DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);

            seed++;
            amp *= _gain;
            freq *= _lacunarity;
        }
    }

    private void DomainWarpFractalProgressive(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        for (int i = 0; i < _octaves; i++)
        {
            FNLfloat xs = x;
            FNLfloat ys = y;
            FNLfloat zs = z;
            TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

            DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);

            seed++;
            amp *= _gain;
            freq *= _lacunarity;
        }
    }


    // Domain Warp Fractal Independant
    private void DomainWarpFractalIndependent(ref FNLfloat x, ref FNLfloat y)
    {
        FNLfloat xs = x;
        FNLfloat ys = y;
        TransformDomainWarpCoordinate(ref xs, ref ys);

        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        for (int i = 0; i < _octaves; i++)
        {
            DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);

            seed++;
            amp *= _gain;
            freq *= _lacunarity;
        }
    }

    private void DomainWarpFractalIndependent(ref FNLfloat x, ref FNLfloat y, ref FNLfloat z)
    {
        FNLfloat xs = x;
        FNLfloat ys = y;
        FNLfloat zs = z;
        TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);

        int seed = _seed;
        float amp = _domainWarpAmp * _fractalBounding;
        float freq = _frequency;

        for (int i = 0; i < _octaves; i++)
        {
            DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);

            seed++;
            amp *= _gain;
            freq *= _lacunarity;
        }
    }


    // Domain Warp Basic Grid

    private void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, FNLfloat x, FNLfloat y, ref FNLfloat xr, ref FNLfloat yr)
    {
        FNLfloat xf = x * frequency;
        FNLfloat yf = y * frequency;

        int x0 = FastFloor(xf);
        int y0 = FastFloor(yf);

        float xs = InterpHermite((float)(xf - x0));
        float ys = InterpHermite((float)(yf - y0));

        x0 *= PrimeX;
        y0 *= PrimeY;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;

        int hash0 = Hash(seed, x0, y0) & (255 << 1);
        int hash1 = Hash(seed, x1, y0) & (255 << 1);

        float lx0x = Lerp(FastNoiseLiteTables.RandVecs2D[hash0], FastNoiseLiteTables.RandVecs2D[hash1], xs);
        float ly0x = Lerp(FastNoiseLiteTables.RandVecs2D[hash0 | 1], FastNoiseLiteTables.RandVecs2D[hash1 | 1], xs);

        hash0 = Hash(seed, x0, y1) & (255 << 1);
        hash1 = Hash(seed, x1, y1) & (255 << 1);

        float lx1x = Lerp(FastNoiseLiteTables.RandVecs2D[hash0], FastNoiseLiteTables.RandVecs2D[hash1], xs);
        float ly1x = Lerp(FastNoiseLiteTables.RandVecs2D[hash0 | 1], FastNoiseLiteTables.RandVecs2D[hash1 | 1], xs);

        xr += Lerp(lx0x, lx1x, ys) * warpAmp;
        yr += Lerp(ly0x, ly1x, ys) * warpAmp;
    }

    private void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, FNLfloat x, FNLfloat y, FNLfloat z, ref FNLfloat xr, ref FNLfloat yr, ref FNLfloat zr)
    {
        FNLfloat xf = x * frequency;
        FNLfloat yf = y * frequency;
        FNLfloat zf = z * frequency;

        int x0 = FastFloor(xf);
        int y0 = FastFloor(yf);
        int z0 = FastFloor(zf);

        float xs = InterpHermite((float)(xf - x0));
        float ys = InterpHermite((float)(yf - y0));
        float zs = InterpHermite((float)(zf - z0));

        x0 *= PrimeX;
        y0 *= PrimeY;
        z0 *= PrimeZ;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;
        int z1 = z0 + PrimeZ;

        int hash0 = Hash(seed, x0, y0, z0) & (255 << 2);
        int hash1 = Hash(seed, x1, y0, z0) & (255 << 2);

        float lx0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0], FastNoiseLiteTables.RandVecs3D[hash1], xs);
        float ly0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 1], FastNoiseLiteTables.RandVecs3D[hash1 | 1], xs);
        float lz0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 2], FastNoiseLiteTables.RandVecs3D[hash1 | 2], xs);

        hash0 = Hash(seed, x0, y1, z0) & (255 << 2);
        hash1 = Hash(seed, x1, y1, z0) & (255 << 2);

        float lx1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0], FastNoiseLiteTables.RandVecs3D[hash1], xs);
        float ly1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 1], FastNoiseLiteTables.RandVecs3D[hash1 | 1], xs);
        float lz1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 2], FastNoiseLiteTables.RandVecs3D[hash1 | 2], xs);

        float lx0y = Lerp(lx0x, lx1x, ys);
        float ly0y = Lerp(ly0x, ly1x, ys);
        float lz0y = Lerp(lz0x, lz1x, ys);

        hash0 = Hash(seed, x0, y0, z1) & (255 << 2);
        hash1 = Hash(seed, x1, y0, z1) & (255 << 2);

        lx0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0], FastNoiseLiteTables.RandVecs3D[hash1], xs);
        ly0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 1], FastNoiseLiteTables.RandVecs3D[hash1 | 1], xs);
        lz0x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 2], FastNoiseLiteTables.RandVecs3D[hash1 | 2], xs);

        hash0 = Hash(seed, x0, y1, z1) & (255 << 2);
        hash1 = Hash(seed, x1, y1, z1) & (255 << 2);

        lx1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0], FastNoiseLiteTables.RandVecs3D[hash1], xs);
        ly1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 1], FastNoiseLiteTables.RandVecs3D[hash1 | 1], xs);
        lz1x = Lerp(FastNoiseLiteTables.RandVecs3D[hash0 | 2], FastNoiseLiteTables.RandVecs3D[hash1 | 2], xs);

        xr += Lerp(lx0y, Lerp(lx0x, lx1x, ys), zs) * warpAmp;
        yr += Lerp(ly0y, Lerp(ly0x, ly1x, ys), zs) * warpAmp;
        zr += Lerp(lz0y, Lerp(lz0x, lz1x, ys), zs) * warpAmp;
    }


    // Domain Warp Simplex/OpenSimplex2
    private void SingleDomainWarpSimplexGradient(int seed, float warpAmp, float frequency, FNLfloat x, FNLfloat y, ref FNLfloat xr, ref FNLfloat yr, bool outGradOnly)
    {
        const float SQRT3 = 1.7320508075688772935274463415059f;
        const float G2 = (3 - SQRT3) / 6;

        x *= frequency;
        y *= frequency;

        /*
         * --- Skew moved to TransformNoiseCoordinate method ---
         * const FNfloat F2 = 0.5f * (SQRT3 - 1);
         * FNfloat s = (x + y) * F2;
         * x += s; y += s;
        */

        int i = FastFloor(x);
        int j = FastFloor(y);
        float xi = (float)(x - i);
        float yi = (float)(y - j);

        float t = (xi + yi) * G2;
        float x0 = (float)(xi - t);
        float y0 = (float)(yi - t);

        i *= PrimeX;
        j *= PrimeY;

        float vx, vy;
        vx = vy = 0;

        float a = 0.5f - x0 * x0 - y0 * y0;
        if (a > 0)
        {
            float aaaa = (a * a) * (a * a);
            float xo, yo;
            if (outGradOnly)
                GradCoordOut(seed, i, j, out xo, out yo);
            else
                GradCoordDual(seed, i, j, x0, y0, out xo, out yo);
            vx += aaaa * xo;
            vy += aaaa * yo;
        }

        float c = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a);
        if (c > 0)
        {
            float x2 = x0 + (2 * (float)G2 - 1);
            float y2 = y0 + (2 * (float)G2 - 1);
            float cccc = (c * c) * (c * c);
            float xo, yo;
            if (outGradOnly)
                GradCoordOut(seed, i + PrimeX, j + PrimeY, out xo, out yo);
            else
                GradCoordDual(seed, i + PrimeX, j + PrimeY, x2, y2, out xo, out yo);
            vx += cccc * xo;
            vy += cccc * yo;
        }

        if (y0 > x0)
        {
            float x1 = x0 + (float)G2;
            float y1 = y0 + ((float)G2 - 1);
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b > 0)
            {
                float bbbb = (b * b) * (b * b);
                float xo, yo;
                if (outGradOnly)
                    GradCoordOut(seed, i, j + PrimeY, out xo, out yo);
                else
                    GradCoordDual(seed, i, j + PrimeY, x1, y1, out xo, out yo);
                vx += bbbb * xo;
                vy += bbbb * yo;
            }
        }
        else
        {
            float x1 = x0 + ((float)G2 - 1);
            float y1 = y0 + (float)G2;
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b > 0)
            {
                float bbbb = (b * b) * (b * b);
                float xo, yo;
                if (outGradOnly)
                    GradCoordOut(seed, i + PrimeX, j, out xo, out yo);
                else
                    GradCoordDual(seed, i + PrimeX, j, x1, y1, out xo, out yo);
                vx += bbbb * xo;
                vy += bbbb * yo;
            }
        }

        xr += vx * warpAmp;
        yr += vy * warpAmp;
    }

    private void SingleDomainWarpOpenSimplex2Gradient(int seed, float warpAmp, float frequency, FNLfloat x, FNLfloat y, FNLfloat z, ref FNLfloat xr, ref FNLfloat yr, ref FNLfloat zr, bool outGradOnly)
    {
        x *= frequency;
        y *= frequency;
        z *= frequency;

        /*
         * --- Rotation moved to TransformDomainWarpCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = FastRound(x);
        int j = FastRound(y);
        int k = FastRound(z);
        float x0 = (float)x - i;
        float y0 = (float)y - j;
        float z0 = (float)z - k;

        int xNSign = (int)(-x0 - 1.0f) | 1;
        int yNSign = (int)(-y0 - 1.0f) | 1;
        int zNSign = (int)(-z0 - 1.0f) | 1;

        float ax0 = xNSign * -x0;
        float ay0 = yNSign * -y0;
        float az0 = zNSign * -z0;

        i *= PrimeX;
        j *= PrimeY;
        k *= PrimeZ;

        float vx, vy, vz;
        vx = vy = vz = 0;

        float a = (0.6f - x0 * x0) - (y0 * y0 + z0 * z0);
        for (int l = 0; ; l++)
        {
            if (a > 0)
            {
                float aaaa = (a * a) * (a * a);
                float xo, yo, zo;
                if (outGradOnly)
                    GradCoordOut(seed, i, j, k, out xo, out yo, out zo);
                else
                    GradCoordDual(seed, i, j, k, x0, y0, z0, out xo, out yo, out zo);
                vx += aaaa * xo;
                vy += aaaa * yo;
                vz += aaaa * zo;
            }

            float b = a;
            int i1 = i;
            int j1 = j;
            int k1 = k;
            float x1 = x0;
            float y1 = y0;
            float z1 = z0;

            if (ax0 >= ay0 && ax0 >= az0)
            {
                x1 += xNSign;
                b = b + ax0 + ax0;
                i1 -= xNSign * PrimeX;
            }
            else if (ay0 > ax0 && ay0 >= az0)
            {
                y1 += yNSign;
                b = b + ay0 + ay0;
                j1 -= yNSign * PrimeY;
            }
            else
            {
                z1 += zNSign;
                b = b + az0 + az0;
                k1 -= zNSign * PrimeZ;
            }

            if (b > 1)
            {
                b -= 1;
                float bbbb = (b * b) * (b * b);
                float xo, yo, zo;
                if (outGradOnly)
                    GradCoordOut(seed, i1, j1, k1, out xo, out yo, out zo);
                else
                    GradCoordDual(seed, i1, j1, k1, x1, y1, z1, out xo, out yo, out zo);
                vx += bbbb * xo;
                vy += bbbb * yo;
                vz += bbbb * zo;
            }

            if (l == 1) break;

            ax0 = 0.5f - ax0;
            ay0 = 0.5f - ay0;
            az0 = 0.5f - az0;

            x0 = xNSign * ax0;
            y0 = yNSign * ay0;
            z0 = zNSign * az0;

            a += (0.75f - ax0) - (ay0 + az0);

            i += (xNSign >> 1) & PrimeX;
            j += (yNSign >> 1) & PrimeY;
            k += (zNSign >> 1) & PrimeZ;

            xNSign = -xNSign;
            yNSign = -yNSign;
            zNSign = -zNSign;

            seed += 1293373;
        }

        xr += vx * warpAmp;
        yr += vy * warpAmp;
        zr += vz * warpAmp;
    }
}