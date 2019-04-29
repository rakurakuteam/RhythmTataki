#include <SoftwareSerial.h>

SoftwareSerial BTSerial(1, 2);

#define PIEZO1 A0
#define PIEZO2 A1

//채터링 방지용 딜레이 시간
#define DelayTime 100
// 센서 입력크리 power 이상의 값이 입력됬을때 출력ㄴ
#define Power 50

//1번드럼 red 핀넘버
#define Red1 3
#define Green1 5
#define Blue1 6

//2번드럼 핀넘버
#define Red2 9
#define Green2 10
#define Blue2 11

unsigned long lastTime1 = 0;
unsigned long lastTime2 = 0;

unsigned long curTime1;
unsigned long curTime2;

int sensor1;
int sensor2;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  BTSerial.begin(9600);
}

void loop() {
  
  sensor1 = analogRead(PIEZO1);
  sensor2 = analogRead(PIEZO2);

  curTime1 = millis();
  curTime2 = curTime1;
  
  if(sensor1 > Power && (curTime1 - lastTime1) > DelayTime ){
    BTSerial.println("쿵");
    BTSerial.println(sensor1);
    lastTime1 = millis();
    LEDon(1, sensor1/4, 0, 0);
  }
  
  if(sensor2 > Power && (curTime2 - lastTime2) > DelayTime ){
    BTSerial.println("짝");
    BTSerial.println(sensor2);
    lastTime2 = millis();
    LEDon(2, 0, 0, sensor2/4);
  }
}


//red 함수
void LEDon(int led, int r, int g, int b){
  if(led == 1){
    analogWrite(Red1, r);
    analogWrite(Green1, g);
    analogWrite(Blue1, b);
    
  }else{
    analogWrite(Red2, r);
    analogWrite(Green2, g);
    analogWrite(Blue2, b);
  }
}
