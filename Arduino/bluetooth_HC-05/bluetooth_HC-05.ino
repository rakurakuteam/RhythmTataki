#include <SoftwareSerial.h>
SoftwareSerial BTSerial(4,5);

void setup() {
  // put your setup code here, to run once:
  BTSerial.begin(9600);
  Serial.begin(9600);
}

void loop() {
  if(Serial.available()){
    BTSerial.write(Serial.read());
  }
  if(BTSerial.available()){
    Serial.write(BTSerial.read());
  }

}
