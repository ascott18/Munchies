- music.mod is the original audio file. 
- music.mod.mp4 is that file converted to a more usable format using VLC
- music.ogg is the mp4 converted to an ogg (to match all the other sounds). ` ffmpeg -i .\Music.mod.mp4 -ar 24000 music.ogg`

The current code requires sample rates of all the files to match. We're currently not doing any on-demand resampling, so sample rates must be 24000 (all the sounds already were 24000 when I encountered this issue, with the sole exception of the music which must have its sample rate specified explicitly.)

The original source of the music (not where I found it, but the ultimate real original copy of the music) can be found at http://janeway.exotica.org.uk/release.php?id=36760 or https://www.youtube.com/watch?v=t07Cm-1y-RQ