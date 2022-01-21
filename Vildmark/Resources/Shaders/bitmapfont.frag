#version 460

uniform sampler2D textures[8];
uniform vec4 tint;

in vec2 vert_frag_texcoord;
flat in int vert_frag_texture_index;

layout(location = 0) out vec4 frag_color;

void main()
{
	frag_color = texture(textures[vert_frag_texture_index], vert_frag_texcoord) * tint;
}
