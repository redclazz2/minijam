sampler2D inputTexture : register(s0);

float2 screenSize;
float time;

float4 main(float2 uv : TEXCOORD0) : COLOR0
{
    // Stronger barrel distortion
    float2 centeredUV = uv * 2.0 - 1.0;
    float2 distortedUV = centeredUV + 0.1 * centeredUV * float2(centeredUV.y * centeredUV.y, centeredUV.x * centeredUV.x);
    distortedUV = (distortedUV + 1.0) / 2.0;
    distortedUV = saturate(distortedUV);

    // Stronger RGB channel offset (color fringing)
    float2 offset = 0.003 * float2(sin(time * 6.0), cos(time * 4.0));

    float r = tex2D(inputTexture, distortedUV + offset).r;
    float g = tex2D(inputTexture, distortedUV).g;
    float b = tex2D(inputTexture, distortedUV - offset).b;
    float3 color = float3(r, g, b);

    // Visible scanlines (stronger)
    float scanline = 0.6 + 0.4 * sin(uv.y * screenSize.y * 3.1415 * 1.5 + time * 10.0);
    color *= scanline;

    // More dramatic vignette
    float2 toCenter = uv - 0.5;
    float vignette = smoothstep(0.7, 0.4, length(toCenter));
    color *= vignette;

    return float4(color, 1.0);
}

technique CRT
{
    pass P0
    {
        PixelShader = compile ps_3_0 main();
    }
}
