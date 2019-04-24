#define PIEZO1 2
#define PIEZO2 3
#define DelayTime 100

unsigned long lastTime1 = 0;
unsigned long lastTime2 = 0;

unsigned long curTime1;
unsigned long curTime2;

//---------------------interrupt function---------------------------------
void drum1(){
  curTime1 = millis();
  
  if((curTime1 - lastTime1) > DelayTime){
    Serial.println(digitalRead(PIEZO1));
    Serial.println("********************");
    Serial.print(lastTime1);
    Serial.print(" : ");
    Serial.print(millis());
    Serial.println("");
    
    lastTime1 = millis();
  }
}

void drum2(){
  //if(((millis() - lastTime) > DelayTime) ||((lastTime - millis()) >= 0)){
  curTime2 = millis();
  
  if((curTime2 - lastTime2) > DelayTime){
    Serial.println(PIEZO2);
    Serial.println("                          ********************");
    Serial.print(lastTime2);
    Serial.print(" : ");
    Serial.println(millis());
    Serial.println("");
    
  lastTime2 = millis();
  }
}
//------------------------ set up -----------------------------------------
void setup() {
  Serial.begin(9600);
  attachInterrupt(0, drum1, RISING);
  attachInterrupt(1, drum2, RISING);
}
//--------------------------- loop ---------------------------------------------------------
void loop() {

}
