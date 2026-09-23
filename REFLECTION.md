# Reflection: Copilot을 활용한 InventoryHub 개발 과정

## 1. Copilot이 각 단계에서 어떻게 도움을 주었는지

- **통합 코드 생성**: Blazor 프론트엔드(FetchProducts.razor)와 Minimal API 백엔드 간
  HTTP 통신 코드를 작성할 때, Copilot이 HttpClient 호출 패턴과 비동기 처리 구조를
  빠르게 제안해주어 기본 골격을 잡는 시간을 단축시켰다.
- **문제 디버깅**: API 경로 불일치(/api/products vs /api/productlist), CORS 설정
  누락, JSON 역직렬화 오류 등을 겪었을 때, Copilot이 정확한 원인과 수정 코드를
  제시해 문제 해결 속도를 높였다.
- **JSON 응답 구조화**: 상품 데이터에 category라는 중첩 객체를 추가할 때, Copilot이
  클래스 설계(Category 클래스 분리)와 직렬화 옵션(PropertyNameCaseInsensitive)을
  제안해 표준화된 JSON 구조를 쉽게 구현할 수 있었다.
- **성능 최적화**: 백엔드 응답에 Cache-Control 헤더를 추가하는 캐싱 전략과, 프론트엔드에서
  API를 OnInitializedAsync에서 한 번만 호출하도록 구성하는 방식을 Copilot이 제안했다.

## 2. 직면한 어려움과 Copilot의 도움

- 터미널에서 서버를 여러 개 동시에 실행해야 하는 상황(백엔드+프론트엔드)에서 처음에는
  같은 터미널에 명령어를 겹쳐 입력해 서버가 꺼지는 문제를 겪었다. 이 과정에서 각
  프로세스가 독립적인 터미널 세션을 필요로 한다는 점을 이해하게 되었다.
- 컨트롤러 통합(InventoryController + UsersController → DataController) 과정에서
  라우트 충돌 문제가 있었는데, Copilot이 하위 경로 분리(api/data/inventory,
  api/data/users) 방식을 제안해 해결했다.

## 3. 풀스택 개발에서 Copilot을 효과적으로 사용하는 방법

- Copilot은 특히 반복적인 CRUD 코드, 예외 처리 패턴, JSON 직렬화 설정과 같이 정형화된
  코드를 빠르게 작성하는 데 유용했다.
- 다만 프로젝트 구조나 폴더 이동 같은 환경 설정 문제는 Copilot보다 직접 확인하고
  단계별로 검증하는 것이 중요했다.
- 프론트엔드와 백엔드가 분리된 풀스택 환경에서는, 각 계층에서 Copilot에게 맥락
  (엔드포인트 경로, 데이터 모델)을 명확히 알려줄수록 더 정확한 제안을 받을 수 있었다.