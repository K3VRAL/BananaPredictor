# Name

BananaPredictor - An application to predict where bananas will be placed for osu!ctb maps.

# Synopsis

bnprdctr

# Description

BananaPredictor is an application made to allow for the user to predict and place bananas in specific areas that they want. This is done so with the help of sliders as they help allow the spinner to change it's seed to be placed in another area. Because of how many sliders, droplets, or tiny droplets exists, we can be able to influence where the banana will end up based on how many times we can keep putting in these seed affecting objects.

# Resources

Some arguments are mandatory, and so they will be labeled with a `*`.

## Start (`-s` or `--start`)

The start of the Banana Predictor. This will place the whole contents within the Banana Predictor to be placed in the part of the map where it starts.

If no argument is provided; the default value is `0`ms.

## Length (`-l` or `--length`) - Unsigned Integer

The length of of the Banana Predictor. This will draw out how long the Banana Predictor is, specifically so the inverter argument knows when to stop (or it would carry on forever). If a point goes above the time value for the length, that will be the new value set for the length.

If no argument is provided; the default value is `0`ms.

## Banana Shower Length (`-r` or `--shower-length`) - Unsigned Integer

When placing the Banana Showers, we would want to give the length for each shower to be, whether to draw out it's length and have more bananas or make it smaller (the minimum always being 2 bananas each shower created).

If no argument is provided; the default value is `1`ms.

## Banana Shower Distance (`-d` or `--shower-distance`) - Unsigned Integer

When placing the next Banana Shower relative from the previous one, we would want to give it a distance of how far until the next placement.

If no argument is provided; the default value is `0`ms.

## Invert (`-i` or `--invert`) - Included or Not

If no argument is provided; the default value is `false`.

## *Points (`-p` or `--points`)

The format for a point is `x:time[:bezier]|`. The final point will always go back to the first point.

### *x - unsigned integer

Is the x-axis for the point. It can go from 0 to 256 as that's how wide osu maps can be.

### *time - unsigned integer

Is the time for the point relative to the spinner predictor's start time. If the point's time is bigger than the length of the spinner predictor, then the point's time will override it.

### bezier - Included (`0` or `1`) or Not

Is whether or not to make the point have the bezier property. If it is 0, it will not include the bezier property meaning that in a point of time, the point will not influence the line of the previous point to the next point. If it is 1, it will include the bezier property meaning that it will influence the line of the previous point to the next point. (Please [watch this video](https://www.youtube.com/watch?v=pnYccz1Ha34) about bezier curves, I cannot explain it properly).

The first point is mandatorily false (might change in the future).

If no argument is provided; the default value is `0`.

### *:

Is a point specific seperator such that after we are done filling out one property of the point, we can fill out the next property.

### *|

Is a point seperator such that after we are done filling out one point, we can fill out the next point.

# Authors

K 3 V R A L