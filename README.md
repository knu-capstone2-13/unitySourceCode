version https://git-lfs.github.com/spec/v1
oid sha256:00f8fcd5f6d9f5224e9c155d0aad533fb7643975b6702c61a82845984768c775
size 1323
## 준비사항
1. Stable Diffusion WebUI 설치가 되어있어야 한다. 
    <https://github.com/AUTOMATIC1111/stable-diffusion-webui>
2. stable-diffusion-webui 폴더는 unitySourceCode 폴더와 같은 디렉토리에 존재해야 한다.
3. webui-user.bat을 편집하여 set COMMANDLINE_ARGS=--xformers --autolaunch --no-half --api --api-log --listen 로 수정한다.
4. LCM-lora를 다운로드 받아 stable-diffusion-webui\models\Lora 안에 저장한다.
    <https://huggingface.co/latent-consistency/lcm-lora-sdv1-5/blob/main/pytorch_lora_weights.safetensors>

## 사용방법
1. Execute WebUI 버튼을 눌러 WebUI가 실행되길 기다린다. WebUI 폴더가 없거나 bat 파일이 없다면 버튼이 비활성화 된다.
2. WebUI가 실행되면 Load 버튼을 눌러 Model과 Lora를 로드한다.
3. Address는 로컬IP가 자동으로 로드된다. 수정이 필요하면 수정한다.
4. port는 7860을 기본으로 사용한다. 상황에 따라 7861~을 사용할 수 있다.
5. Prompt에 원하는 문장을 입력한다.
6. Steps과 CFG Scale에 원하는 값을 입력한다.
7. Model과 Lora에 원하는 파일을 선택한다.
8. Lora를 사용하지 않을 것이라면, 체크박스를 해제한다.
9. Generate 버튼을 눌러 이미지가 생성되길 기다린다.
