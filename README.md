# Cinelights
Example project using (LightingTools.Core)[https://github.com/laurenth-unity/LightingTools.Core] and (LightingTools.CineLights)[https://github.com/laurenth-unity/LightingTools.Cinelights] for cinematic lighting with HD Render Pipeline in Unity 2018.3.0b08+ versions.

UPDATE :
Scripts are no longer living in the projects, they are like features you can opt in by just adding the packages to your project's Packages folder.
In github they are referenced as submodules (if you use the download button you need to download them separately).
Downloading this project now only downloads an example project that uses the packages LightingTools.Core and LightingTools.CineLights.

Tested in Unity 2018.3.0b8 with HD Render Pipeline 4.2.0

# How to use the Cine Lights

1. Entity

- Place an empty gameobject in the scene
- Add a Cinelight component to it
- Place the gameobject Pivot where you want your light to look at, or attach it to another gameobject if you want the light to follow
- Tweak the light settings through the Cinelight inspector parameters

2. Timeline Track

- In an existing timeline add a Cinelight track
- On the left side of the track there is a slot where you can reference an entity that the light will be pointing at : typical use case is a character's hip bone or head bone, this way the light will follow the character.
- Add Cinelight clips on the track to control the Cinelight. When there is no clip, the light is disabled.
- Use Fade-in/Fade-out/Blend between clips to animate your light.
- For more than one light, use more tracks, see provided example.
