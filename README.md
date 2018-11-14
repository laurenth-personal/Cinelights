# Cinelights
Light entity dedicated to cinematic lighting.

Tested in Unity 2018.1b10

# How to use

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
