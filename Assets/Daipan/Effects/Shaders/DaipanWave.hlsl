#pragma once

void Wave_float(float4 uv, float2 size, float2 center, float radius, float width, out float4 output, out float alpha)
{
    uv.x *= size.x / size.y;
    center.x *= size.x / size.y;
    // radius = frac(radius);
    // radius = max(0,radius - 10);
    // 極座標
    float2 ref;
    float2 val = uv.xy - center;
    ref.x = length(val);
    ref.y = atan2(val.y,val.x);
    
    // 衝撃による揺らぎ
    // ref.x += (sin(radius)) * step(radius, ref.x) * step(ref.x, radius + width) * 0.2;
    alpha = smoothstep(radius,radius + width,ref.x) * step(ref.x, radius + width);
    // ref.x += (sin(radius)) * smoothstep(radius,radius + width,ref.x) * step(ref.x, radius + width) * 0.2;
    ref.x += (sin(radius)) * 0.2 * alpha;

    // alpha = 1;
    // ref.x += (sin(radius)) * smoothstep(radius,radius + width,ref.x) * step(ref.x, radius + width) * 0.2;
    // 直行座標
    uv.x = ref.x * cos(ref.y);
    uv.y = ref.x * sin(ref.y);
    uv.xy += center;
    
    uv.x *= size.y / size.x;
    output = uv;
}