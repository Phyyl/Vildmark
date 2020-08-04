#version 330
precision highp float;

uniform sampler2D tex0;
uniform vec4 tint;

in vec2 vert_frag_tex_coord;
in vec4 vert_frag_color;

layout(location = 0) out vec4 frag_color;

void main()
{
	float sample = texture(tex0, vert_frag_tex_coord).r;
    
    float scale = 1.0 / fwidth(sample);
    float signedDistance = (sample - 0.5) * scale;
      
    float color = clamp(signedDistance + 0.5, 0.0, 1.0);
    float alpha = clamp(signedDistance + 0.5 + scale * 0.125, 0.0, 1.0);
    float result = color * alpha;
    frag_color = vec4(result, result, result, result) * vert_frag_color * tint;
}