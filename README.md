# 🏭 Vision Work Order App



## 📌 프로젝트 소개

> 제조 현장의 작업지시 관리와 OpenCV 비전 검사를 통합한 MES 애플리케이션입니다.
> 카메라로 제품을 촬영하여 빨간 사과의 품질을 자동으로 판별하고 결과를 DB에 저장합니다.





## 📌 목차

### 프로젝트 개요
- [① 프로젝트 소개](#-프로젝트-소개)
- [② 개발 기간](#-개발-기간)
- [③ 기술 스택](#-기술-스택)

### UI & 기능 소개
- [④ 주요 기능](#-주요-기능)
- [⑤ 화면 구성](#-화면-스크린샷)

### 기타
- [⑥ 트러블슈팅](#-트러블슈팅)
- [⑦ 실행 방법](#-실행-방법)


## 📅 개발 기간

2026.02 ~ 2026.03

## 🛠 기술 스택

| 분류 | 기술 |
|------|------|
| UI | WPF (.NET Framework 4.8) |
| 아키텍처 | MVVM 패턴 |
| DB | MSSQL (SQL Server Express) |
| ORM | Entity Framework 6 |
| 영상처리 | OpenCvSharp4 |


## ✅ 주요 기능
- WPF + MVVM 패턴으로 UI 와 비즈니스 로직 분리
- Entity Framework 6 + MSSQL 로 DB 연동
- OpenCvSharp4 로 실시간 카메라 영상처리 및 자동 품질 판별 구현
### 1. 작업지시 관리
- 작업지시 CRUD (추가 / 수정 / 삭제) 구현
- Entity Framework 6 + MSSQL 연동으로 데이터 영구 저장
- ObservableCollection 으로 DB 변경사항 즉시 UI 반영
- EF 변경 추적 활용으로 기존 객체 직접 수정 후 SaveChanges() 호출
- ComboBox 바인딩으로 설비 선택 기능 구현
- 추가 <br>
![ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/3f10f630-cb2f-4869-af0e-08b1bd9c596a)
- 수정 <br>
![작업지시 수정](https://github.com/user-attachments/assets/c07cbe37-4ca2-4aff-8636-181ba4878c2d)
- 삭제 <br>
![작업지시 삭제](https://github.com/user-attachments/assets/4cffe3bf-137c-4228-b506-2d2eda0146c3)



### 2. 비전 검사 실행
- 실시간 카메라 영상 출력
- HSV 색상 변환으로 빨간색 감지
- 빨간색 비율 기반 자동 OK/NG 판별
- 검사 결과 자동 DB 저장 (1초 간격)

### 3. 검사 결과 이력
- DB에서 검사 결과 조회
- 전체 / OK / NG 필터링

## 💡 트러블슈팅

### EF 변경 추적 미인식으로 수정 DB 미반영
- **문제** : 수정 버튼 클릭 시 ObservableCollection의 화면 목록은 바뀌지만 DB에 반영되지 않음
- **원인** : EF는 DbContext로 조회한 객체만 내부적으로 추적(Tracking) 상태로 관리함.
  수정 시 새 WorkOrder 객체를 생성해 WorkOrders[index]에 교체하면
  화면(ObservableCollection)에는 반영되지만,
  새 객체는 DbContext가 모르는 Detached 상태이므로
  SaveChanges() 호출 시 UPDATE 쿼리가 실행되지 않음
  ```csharp
  // 문제가 된 코드
  int index = WorkOrders.IndexOf(SelectedWorkOrder);
  WorkOrders[index] = new WorkOrder(NewProductName, NewQuantity, NewStatus, NewEquipmentName);
  _db.SaveChanges(); // EF가 새 객체를 모르기 때문에 DB 반영 안됨

### 카메라 스레드에서 UI 업데이트 시 InvalidOperationException
- **문제** : 카메라 루프 스레드에서 BitmapSource 프로퍼티를 직접 업데이트하면
  InvalidOperationException 발생
- **원인** : WPF의 UI 요소는 UI 스레드(메인 스레드)에서만 접근 가능.
  StartCamera()에서 별도 Thread를 생성해 CameraLoop()를 실행하는데,
  이 스레드에서 BitmapSource = flipped.ToBitmapSource() 를 직접 호출하면
  UI 스레드가 아닌 백그라운드 스레드에서 UI 프로퍼티에 접근하는 것이므로
  WPF가 예외를 발생시킴
  ```csharp
  // 문제가 된 코드 (카메라 스레드에서 직접 UI 업데이트)
  private void CameraLoop()
  {
      while (_isRunning)
      {
          videoCapture.Read(frame);
          UpdateFrame(); // 백그라운드 스레드에서 직접 호출 → 예외 발생
          Thread.Sleep(33);
      }
  }


### 수정 후 DataGrid UI 미갱신
- **문제** : 수정 버튼 클릭 시 DB는 저장되지만 DataGrid 행이 변경되지 않음
- **원인** : WorkOrder 모델이 INotifyPropertyChanged를 구현하지 않아
  SelectedWorkOrder.ProductName = NewProductName 으로 값을 바꿔도
  DataGrid 바인딩이 변경을 감지하지 못함.
  ObservableCollection은 항목 추가/삭제만 알리고,
  항목 내부 프로퍼티 변경은 해당 객체가 직접 알려야 함
- **시도** : 코드 중복을 줄이려고 WorkOrder에 BaseViewModel 상속 시도.
  동작은 되지만 Models 폴더가 ViewModels 폴더를 참조하게 되어
  Model → ViewModel 의존이 발생. MVVM 계층 구조가 무너지고
  프로젝트 규모가 커질 경우 순환 참조 위험이 있어 적용하지 않음
- **해결** : WorkOrder에 INotifyPropertyChanged 직접 구현 후
  각 프로퍼티 setter에 OnPropertyChanged() 추가
  → DataGrid 컬럼이 ProductName, Quantity 등에 바인딩되어 있으므로
  값 변경 시 즉시 화면 갱신
  
### 다른 메뉴 이동 시 카메라 백그라운드 스레드 미정지로 DB 계속 저장
- **문제** : 비전 검사 화면에서 다른 메뉴로 이동해도 카메라 스레드가 계속 실행되어
  DB에 검사 결과가 무한 저장됨
- **원인** : InspectionSessionViewModel 생성자에서 StartCamera()를 자동 호출하여
  백그라운드 스레드가 시작됨.
  다른 메뉴로 이동 시 View만 교체되고 ViewModel은 GC 대상이 되지만
  스레드가 살아있어 _isRunning이 false로 바뀌지 않음
- **시도** : 메뉴 전환 시 StopCamera()를 호출하는 방식으로 구현.
  동작은 되지만 다른 메뉴에서 작업 중에도 카메라가 꺼져
  검사가 중단되는 문제가 있어 적용하지 않음
- **해결** : ViewModel을 MainWindow의 필드로 보관하여 싱글턴처럼 유지.
  시작/정지 버튼을 추가해 사용자가 직접 카메라를 제어하도록 변경
  → 메뉴 이동과 무관하게 카메라 상태 유지






## 🚀 실행 방법

1. SQL Server Express 설치
2. App.config 에서 DB 연결 문자열 수정
3. 패키지 관리자 콘솔에서 실행
```
Update-Database
```
4. 빌드 후 실행

---

