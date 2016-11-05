Shader "Custom/JstShader" {
	 Properties {
        _Color ("Main Color", Color) = (1,1,1,0.5)
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
        _Emission ("Emmisive Color", Color) = (0,0,0,0)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.7
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _SubTex ("2nd Texture",2D) = "white"{ }
    }

    SubShader {
       Tags { "Queue" = "Transparent"}
       Pass{
        Blend SrcAlpha OneMinusSrcAlpha    //블랜드 옵션이 추가되었습니다. 
         
            Material {
                Diffuse [_Color]
                Ambient [_Color]
            }
            Lighting On
            SetTexture [_MainTex] {
                Combine texture * primary DOUBLE
            }
             SetTexture [_SubTex] {
              ConstantColor[_Color]   //콘스탄트 컬러 값을 적용했습니다. 
             Combine texture lerp(texture) previous ,constant  // 여기에서 보시면 ,를 넣고 constant 값을 가져왔습니다.!!
            }
        }
        
    }
	FallBack "Diffuse"
}
