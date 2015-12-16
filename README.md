# UtilityMod
This is a Cities Skylines UtilityMod for my personal use. All credit for the original mods goes to their authors.

Currently contains the following utilities:

Anarchy (LeftAlt-1) : Based on (Advanced) Road Anarchy by emf and northfacts.
I have fixed the issue where elevated road segments clamp to the ground. As a side effect, extremely steep slopes
are no longer possible but those look unrealistic anyway.

Angles (LeftAlt-2) : Based on Sharp Junction Angles by Thaok.
I have disabled snapping to roads and tracks.

Water (LeftAlt-W) : Based on No Water Check by boformer.

Dev (LeftAlt-D) : Cities Skylines Developer mode.

Snap (LeftAlt-S) : Toggle snap to grid.

As a bonus for developers, the mod supports Unity hotloading: while in game, recompile the mod using Visual
Studio. The game will instantly load and use the recompiled mod dll. Deinitialization and initialization are
performed properly.
