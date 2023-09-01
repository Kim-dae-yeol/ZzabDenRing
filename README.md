# ZzabDenRing

스파르타 코딩클럽 내일배움캠프 C# 심화주차 팀 프로젝트입니다.

## 👥 프로젝트 참여 인원
김대열, 장현교, 최장범, 김어진

## ⚙️ 개발환경
+ .NET 7.0
+ C# 11.0
+ Json 통신

## ▶️ 플레이
  <img width="50%" alt="status" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/115692722/5554d5fb-508f-4163-82e7-61a4629978b2">
<img width="50%" alt="select" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/115692722/5d69c3ff-8e56-4146-ab9c-098254dce0d8">
<img width="50%" alt="main" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/115692722/032188f7-0fdb-4319-8df4-9c4a14fef1d9">
<img width="50%" alt="equipment" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/115692722/2b6ed65b-0184-414a-88af-9c0837b1747b">
<img width="50%" alt="entrance" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/115692722/a7065427-88d9-4d23-9a00-fefebbbe971b">


## 🖼️ 와이어 프레임

<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/c1dec199-40fe-4aa6-b6e3-73c1743fe96a"/>
<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/f444373f-1492-45e0-849b-7cddf1b9f9b6"/>
<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/5457b5c7-b7dd-4970-af69-145f44a75569"/>
<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/3f8e02f2-10af-43e3-ae42-555187bb6e44"/>
<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/de681556-3ff1-4096-bbd4-2632bc1f8da1"/>
<img width="70%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/7d97ffa2-3ee0-4a2a-8020-b607d2c8f787"/>
<img width="50%" src="https://github.com/Kim-dae-yeol/ZzabDenRing/assets/141602161/d32e8d7f-f020-4b6a-94c9-04237ab47d20"/>

## 기능

#### 홈 화면
***
+ 스플래시
+ 캐릭터 선택
+ 캐릭터 생성 및 삭제 (이름, 직업, 능력치)
+ 종료

#### 메인 화면
***
1. 상태보기
2. 던전입장
3. 인벤토리
4. 상점
5. 장비창
x. 나가기

#### 1. 상태창
***
+ 캐릭터 정보
+ 나가기

#### 2. 던전입장
***
+ 조우 
  + 전투정보(몬스터)
  + 전투 / 다른 곳 둘러보기 / 도망
+ 전투화면
  + 전투정보(플레이어/몬스터)
+ 전투종료화면
  + 현재 상태
  + 획득한 아이템
  + 계속 전투 / 마을로

#### 3. 인벤토리
***
+ 아이템 목록
+ 소유한 골드
+ 나가기

#### 4. 상점
***
+ 장비 구매 및 판매
+ 강화
+ 소유한 골드
+ 나가기

#### 5. 장비창
***
+ 각 아이템 유형으로 구별된 장비창
+ 가지고 있는 장비
+ 나가기

## 버그 리포트 [ 현상 ][ 버그의 진입조건 ][ 예상되는 문제점] - [  담당자  ]

- [x]  캐릭터 선택창에서 삭제가 되지 않음 / 스플래시 이후 캐릭터 선택창에서 d키를 입력  → 삭제기능을 삭제 ?
- [x]  스플래시 - 윈도우 환경에서 ? 로 출력됨   - 장범
- [x]  메인 스크린에서 상태창 들어갔다가 메인화면으로 다시와서 시작화면 버튼 누르면 상태창으로 가지는 오류 -장범
- [x]  상태창에서 메인화면으로 나가지지가 않음 / 캐릭터 선택 > 상태 > 0번 입력   - 장범
- [x]  상태창에 선택된 캐릭터 정보 동기화 안됨 -장범
- [x]  상점에서 소모품/재료 /강화 에서 1/0 으로 출력됨 (uiState 수정)- 대열
- [x]  상점에서 강화가 동작하지 않음
- [x]  인벤토리에서 0,0~10,10 밖으로 이동시 예외로 종료 / 각 방향 함수들에 if문을 넣어서 해결해야 함 - 김어진
- [x]  던전 전투에서 진행이 안됨  -현규
- [x]  던전 전투에서 도망갈 경우 비정상 종료 - 현규
- [x]  던전 전투씬 ui 출력 player…같은 방식으로 나옴 - 현규
- [x]  [ 인벤토리 페이지 정렬이 맞지 않음 ]
- [x]  장비창 개발용 데이터 입력되어있음. -대열
- [x]  파일이 비정상 종료시 손상됨…  해결 힘듬 →
- [x]  캐릭터 생성시 능력치 줄일 경우 스탯이 재생성되지않음. - 대열
- [x]  아이템 파일과 실제 코드의 정수값이 다름- 대열
- [x]  상점에서 구매 판매시 idx + 1 의 물건이 사고 팔림 - 대열
- [x]  장비창 투구 선택후 인벤토리 장착시 갑옷으로 선택됨 - 대열
- [ ]  장비창 옆의 인벤토리 텍스트 정렬 -대열
- [ ]  상태창에서 추가된 공격력이 표기되지 않음.
- [x]  [장비창 비정상 종료 ] 비어있는장비창 엔터 누른 후 다시 또누른경우 - EquipmentViewModel.cs:line 89







