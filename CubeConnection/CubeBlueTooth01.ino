#include <SPI.h>
#include "Cube.h"

rgb_t colours[3] = {
  RED, GREEN, BLUE};

Cube cube;

char incomingBytes[12];
int  ByteIdx = 0;  

void setup(void) {
  //Serial.begin(9600);
  Serial.begin(38400); 
  cube.begin(); // Start on serial port 0 (USB) at 115200 baud
  cube.all(BLACK);  //RGB(0x00, 0x00, 0x00)
}

int htoi (char c) {  //does not check that input is valid
  if (c<='9')
    return c-'0';
  if (c<='F')
    return c-'A'+10;
  if (c<='f')
    return c-'a'+10;
  return 0;
}   


void illusion1() {
  int delay_time = 45;
  cube.all(BLACK);
  cube.set(0,0,0,GREEN); 
  cube.set(3,0,3,GREEN); 
  delay(delay_time);
  cube.all(BLACK); 
  delay(delay_time+40);
  cube.set(0,0,3,GREEN); //RGB(0,0,255)
   cube.set(3,0,0,GREEN);
  delay(delay_time);
  cube.all(BLACK);
  delay(delay_time+40); 
}  


void loop(void) {
  //illusion1();
  //return;
  int z;
  int x;
  int y;
  int red;
  int green;
  int blue;
  char incomingByte;  
  String incomingString;
  if (Serial.available()) {        
    incomingByte = Serial.read();
    // Serial1.write(incomingByte);
    if (incomingByte >=  '0') {
        if (ByteIdx > 10) {
            ByteIdx = 0;
            while (Serial.available()) //something gone wrong dump everything and wait for new data
              incomingByte = Serial.read();
            return;
        }  
        incomingBytes[ByteIdx] =  incomingByte; 
        ByteIdx++;
        if ( (incomingByte == '}') && (ByteIdx == 11) ) { //"{111FF00FF}"; 
         incomingString = String(incomingBytes);
         incomingString.toLowerCase();
         if (incomingString == "{whoareyou}") { // use to search for device on different com ports
           Serial.write("cubeofleds\nv.1.0\n");
           ByteIdx=0;
           return;
         }  
          ByteIdx=0;
          x = incomingBytes[1]-'0';
          y = incomingBytes[2]-'0';
          z = incomingBytes[3]-'0';
          red =   htoi(incomingBytes[4])*16+htoi(incomingBytes[5]);
          green = htoi(incomingBytes[6])*16+htoi(incomingBytes[7]);
          blue =  htoi(incomingBytes[8])*16+htoi(incomingBytes[9]);
          if (x==4)
            cube.all(RGB(red,green,blue));
          else
            cube.set(x,y,z,RGB(red,green,blue)); 
        }
    }
  };  
}


