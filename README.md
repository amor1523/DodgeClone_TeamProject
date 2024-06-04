# DidYouWriteTIL

![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/00bd26f7-2042-41dc-b6ce-8f42fb1efffc)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/b9021dfa-c7c4-4230-96ba-460bc17d5ef1)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/6eb7f197-3cd4-4c92-a8f0-4dad16d906e7)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/4de714a7-2bec-4a23-8313-7dcf11336d0b)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/3c4723b4-7ff1-44b7-9bfe-cb10afc150eb)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/ee5f04aa-09b0-407f-b6d5-1a2dddf6bc12)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/5a80a37b-755c-4ef0-aef3-9112cb90129e)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/986b4f82-9d16-4f3c-b01c-8ed5f0e48446)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/8cf0dd27-38cf-4b49-86fc-df07c555d7ce)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/69c0f864-42c2-42f9-8234-294b760adbe8)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/13d09c8a-ac4c-456d-9a06-a95240cb3ef6)
Figma 링크 : https://www.figma.com/file/mCbnZeYcj0rEZiE6Cep46n?%3Fnode-id=0-1&embed_host=notion&kind=file&t=FbuPCauUs8Kx3VNP-0&viewer=1
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/5b462ac2-22fd-434b-84de-7d55506ddfbe)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/ac9b2c20-ae30-4745-83fa-e0c5bd52da88)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/bff8430a-7e41-4487-88f7-9745cd003526)
[https://vimeo.com/949114422?share=copy](https://youtu.be/oooFX-rTo6Q)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/b05911f5-6efc-4044-850b-24082b3cdbbc)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/63178f72-9d0c-4f73-a459-3a45f1a0df72)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/80cb6245-bf8b-4fb7-b9a2-1ffa26a5bb71)
![image](https://github.com/amor1523/DodgeClone_TeamProject/assets/167174802/c6929842-7800-4fc8-ada3-fb6a4e65d036)

FeedBack - 캐릭터 선택 시 외각선으로 시각적 효과를 주었으면 더 좋았을 것!

게임 개발 입문 팀프로젝트 피드백입니다!
팀원들과 공유 부탁드립니다!! 피드백에 문제가 있다고 느낄 시 바로 달려와주세요~!
- 컨셉이 굉장히 재밌습니다. 게임 시작과 종료를 til 제출, 미루기로 하신 것도 그렇고, 공격이 찌르기인 것도 재밌네요.
- SoundManager에서 파일의 경로와 AssetDatabase.LoadAssetAtPath를 활용해 오디오클립을 최초로 사용할 때 메모리에 올리면서 캐싱하는 시도를 하신 부분은 굉장히 좋은 것 같습니다. 다만 AssetDatabase의 경우 에디터에서는 정상 작동하지만 빌드 후 게임을 실행했을 땐 작동하지 않기 때문에, 에셋번들이나 어드레서블을 활용해 비슷한 기능을 구현해보는 방법을 공부해보시면 더 좋을 것 같습니다.
- 또한, 다른 클래스에서 효과음을 재생할 때 매개변수로 “DrinkPotion.mp3”와 같은 식으로 파일 이름을 직접 문자열로 넘겨 받아오고 있는데, 이는 메서드 호출에 실수가 나올 가능성이 너무 커 보입니다. 사운드 인덱스를 숫자나 enum으로 넘기면 파일 이름 문자열을 생성해주는 중간 메서드를 만들어, 효과음을 호출하는 쪽에서는 파일 이름 자체는 신경쓰지 않아도 되도록 구성해주면 더 좋을 것 같습니다.
- GameManager에서 AddScore 메서드를 AddScore(int amt)와 같이 설정해서 score에 값을 더해주는 부분도 AddScore 메서드 내에서 처리해주는 것이 좋아 보입니다. 지금은 점수에 관여하는 메서드가 AddTIL 하나이기 때문에 별로 문제가 생길 일이 없겠지만, 프로젝트가 커지고 점수를 변경시키는 기능을 추가하다보면 메서드 이름을 보고 기대하게 되는 기능과 실제 기능이 달라 혼동이 생길 수 있습니다.
- 팀프로젝트 고생 많으셨습니다. 숙련 주차도 화이팅입니다!
