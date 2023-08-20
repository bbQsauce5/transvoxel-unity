# transvoxel-unity
transvoxel-unity is a procedural voxel terrain generator for Unity using the Job System and Burst.

The tool is made for academic and demonstration purposes and is in no way considered production ready.

# Requirements
Unity 2022.3.5 or older.

# System features:
- Fast Transvoxel implementation using Unity's Job System and Burst Compiler
- Source code is entirely written in C#
- Chunking and dynamic level of detail using an octree for far view distances
- "Unlimited" blending between textures using texture arrays and triplanar shading
- Collider generation inside jobs off the main thread
  
![worldgen](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/b4e3876d-d79f-4c39-a4fd-526c1ee7c270)
![octree](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/24232c50-1413-4018-803f-ef3166cf8320)

# Future work
- Terrain editing
- Decorations and foliage generation
- Infinite terrain generation

# License
[MIT](https://github.com/bbQsauce5/transvoxel-unity/blob/main/LICENSE)

