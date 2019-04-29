#include <SoftwareSerial.h>

SoftwareSerial BTSerial(4, 5);

#define PIEZO1 A0
#define PIEZO2 A1
#define DelayTime 200

#define Red1 3
#define Green1 5
#define Blue1 6

#define Red2 9
#define Green2 10
#define Blue2 11

unsigned long lastTime1 = 0;
unsigned long lastTime2 = 0;

unsigned long curTime1;
unsigned long curTime2;

int sensor1;
int sensor2;

//---------------------interrupt function---------------------------------
//------------------------ set up -----------------------------------------
void setup() {
  Serial.begin(9600);
  BTSerial.begin(9600);
}
//--------------------------- loop ---------------------------------------------------------
void loop() {
  
  
  sensor1 = analogRead(PIEZO1);
  sensor2 = analogRead(PIEZO2);

  curTime1 = millis();
  curTime2 = curTime1;
  
  if(sensor1 > 20 && (curTime1 - lastTime1) > DelayTime ){
    Serial.println("쿵");
    Serial.println(sensor1);
    BTSerial.println(1);
    //if(sensor1 > 255)
   //   sensor1 = 255;
      
  //  LEDon(1, sensor1, 0, 0);
    lastTime1 = millis();
  }
  
  if(sensor2 > 20 && (curTime2 - lastTime2) > DelayTime ){
    Serial.println("짝");
    Serial.println(sensor2);
    BTSerial.println(2);

 //   if(sensor2 > 255)
 //     sensor2 = 255;
    
  //  LEDon(2, 0,sensor2, 0);
    lastTime2 = millis();
  }
}
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
