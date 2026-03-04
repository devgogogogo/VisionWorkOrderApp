# 🏭 Vision Work Order App

WPF + MVVM + Entity Framework + OpenCV 를 활용한 스마트 팩토리 MES 포트폴리오 프로젝트

---

## 📌 프로젝트 개요

제조 현장의 작업지시 관리와 비전 검사를 통합한 MES(Manufacturing Execution System) 애플리케이션입니다.
카메라로 제품을 촬영하여 빨간 사과의 품질을 자동으로 판별하고 결과를 DB에 저장합니다.

---

## 🛠 기술 스택

| 분류 | 기술 |
|------|------|
| UI | WPF (.NET Framework 4.8) |
| 아키텍처 | MVVM 패턴 |
| DB | MSSQL (SQL Server Express) |
| ORM | Entity Framework 6 |
| 영상처리 | OpenCvSharp4 |

---

## ✅ 주요 기능

### 1. 작업지시 관리
- 작업지시 CRUD (추가 / 수정 / 삭제)
- Entity Framework DB 연동
- 실시간 UI 업데이트 (ObservableCollection)

### 2. 비전 검사 실행
- 실시간 카메라 영상 출력
- HSV 색상 변환으로 빨간색 감지
- 빨간색 비율 기반 자동 OK/NG 판별
- 검사 결과 자동 DB 저장 (1초 간격)

### 3. 검사 결과 이력
- DB에서 검사 결과 조회
- 전체 / OK / NG 필터링

---

## 📸 화면 스크린샷

> 스크린샷 추가 예정

---

## 💡 트러블슈팅

### EF 수정 버그
- 문제 : 새 객체로 교체시 DB에 반영 안됨
- 원인 : EF는 처음 가져온 객체만 추적, 새 객체는 모름
- 해결 : 기존 객체를 직접 수정 후 SaveChanges() 호출

### UI 스레드 오류
- 문제 : 카메라 스레드에서 UI 업데이트시 오류
- 원인 : UI는 메인 스레드에서만 업데이트 가능
- 해결 : Dispatcher.Invoke() 로 메인 스레드에서 실행

---

## 🚀 실행 방법

1. SQL Server Express 설치
2. App.config 에서 DB 연결 문자열 수정
3. 패키지 관리자 콘솔에서 실행
```
   Update-Database
```
4. 빌드 후 실행

---

## 📚 배운 것

- WPF MVVM 패턴 구현
- Entity Framework CRUD 및 변경 추적 이해
- OpenCV 를 활용한 실시간 영상처리
- 멀티스레딩 및 UI 스레드 처리