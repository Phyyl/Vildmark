#version 330
precision highp float;

uniform sampler2D tex0;
uniform vec4 tint;

in vec2 vert_frag_tex_coord;
in vec4 vert_frag_color;

layout(location = 0) out vec4 frag_color;

void main()
{
    vec4 sample = texture(tex0, vert_frag_tex_coord);
    frag_color = sample * vert_frag_color * tint;
}