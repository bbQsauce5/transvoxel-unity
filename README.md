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
  
![octree](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/c7fc7b07-d41f-4313-8839-c25f5f47f456)
![worldgen](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/65050723-3a16-4f10-aa5e-7edc1c1f3cb3)

# Future work
- Terrain editing
- Decorations and foliage generation
- Infinite terrain generation

# License
[MIT](https://github.com/bbQsauce5/transvoxel-unity/blob/main/LICENSE)

