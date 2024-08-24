#pragma once

void Wave_float(float4 uv, float2 size, float2 center, float time, float width, out float4 output, out float alpha)
{
    uv.x *= size.x / size.y;
    center.x *= size.x / size.y;
    time = frac(time);
    // time = max(0,time - 10);
    // 極座標
    float2 ref;
    float2 val = uv.xy - center;
    ref.x = length(val);
    ref.y = atan2(val.y,val.x);
    
    // 衝撃による揺らぎ
    // ref.x += (sin(time)) * step(time, ref.x) * step(ref.x, time + width) * 0.2;
    alpha = smoothstep(time,time + width,ref.x) * step(ref.x, time + width);
    // ref.x += (sin(time)) * smoothstep(time,time + width,ref.x) * step(ref.x, time + width) * 0.2;
    ref.x += (sin(time)) * 0.2 * alpha;

    // alpha = 1;
    // ref.x += (sin(time)) * smoothstep(time,time + width,ref.x) * step(ref.x, time + width) * 0.2;
    // 直行座標
    uv.x = ref.x * cos(ref.y);
    uv.y = ref.x * sin(ref.y);
    uv.xy += center;
    
    uv.x *= size.y / size.x;
    output = uv;
}