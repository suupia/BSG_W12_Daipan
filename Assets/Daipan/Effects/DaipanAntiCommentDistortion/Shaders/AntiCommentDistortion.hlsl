#pragma once

void Distortion_float(float4 uv, float2 size, float2 center, float radius, float width,  out float2 uv_out, out float alpha)
{
    uv.x *= size.x / size.y;
    center.x *= size.x / size.y;
    // 極座標
    float2 ref;
    float2 val = uv.xy - center;
    ref.x = length(val);
    ref.y = atan2(val.y,val.x);

    alpha = step(radius - width, ref.x);

    // 0除算回避
    radius = step(0.001, radius) * radius + step(radius, 0.001) * 0.001;
    width = step(0.001, width) * width + step(width, 0.001) * 0.001;

    float ratio2 = width - (radius - ref.x);
    float ratio1 = ratio2 * radius / width;
    ref.x = step(radius, ref.x) * ref.x + step(ref.x, radius) * ratio1;


    uv.x = ref.x * cos(ref.y);
    uv.y = ref.x * sin(ref.y);
    uv.xy += center;
    
    uv.x *= size.y / size.x;
    uv_out = uv;
}