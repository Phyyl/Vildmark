#version 420

precision highp float;

uniform sampler2D tex;
uniform vec4 background_color;
uniform vec4 foreground_color;
uniform float px_range;

in vec2 vert_frag_texcoord;
out vec4 frag_color;

float screenPxRange()
{
    vec2 unitRange = vec2(px_range)/vec2(textureSize(tex, 0));
    vec2 screenTexSize = vec2(1.0)/fwidth(vert_frag_texcoord);
    return max(0.5*dot(unitRange, screenTexSize), 1.0);
}

float median(float r, float g, float b)
{
    return max(min(r, g), min(max(r, g), b));
}

void main()
{
    vec3 msd = texture(tex, vert_frag_texcoord).rgb;
    float sd = median(msd.r, msd.g, msd.b);
    float screenPxDistance = screenPxRange()*(sd - 0.5);
    float opacity = clamp(screenPxDistance + 0.5, 0.0, 1.0);

    frag_color = mix(background_color, foreground_color, opacity);
}