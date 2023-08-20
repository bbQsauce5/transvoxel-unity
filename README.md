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
![2023-08-20 22-53-47](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/aafe306e-ee4b-49a1-b85b-01594bc46ea6)
![2023-08-20 23-06-29](https://github.com/bbQsauce5/transvoxel-unity/assets/52680084/1044b337-4cb3-4b23-8050-e0450143aa28)

# Future work
- Terrain editing
- Decorations and foliage generation
- Infinite terrain generation

# License
[MIT](https://github.com/bbQsauce5/transvoxel-unity/blob/main/LICENSE)
