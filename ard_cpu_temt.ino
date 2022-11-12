#include <Wire.h>
#include <LiquidCrystal_I2C.h>
#define NUM_LEDS 30
#include "FastLED.h"
#define PIN 2
CRGB leds[NUM_LEDS];

LiquidCrystal_I2C lcd(0x27, 16, 2);
String inData;
int data;
int r, g, b;

void setup() {
  FastLED.addLeds<WS2811, PIN, GRB>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
  FastLED.setBrightness(170);
  pinMode(13, OUTPUT);
  Serial.begin(500000);
  lcd.init();
  lcd.backlight();
  lcd.setCursor(0, 0);
  lcd.print("Waiting for");
  lcd.setCursor(0, 1);
  lcd.print("connection...");
}

void loop() {

  while (Serial.available() > 0)
  {
    char recieved = Serial.read();
    inData += recieved;

    if (recieved == 'c') {
      inData = "";
      FastLED.setBrightness(170);
      FastLED.show();
    }

    if (recieved == '*')
    {
      lcd.clear();
      inData.remove(inData.length() - 1, 1);
      lcd.setCursor(0, 0);
      lcd.print("GPU: " + inData + char(223) + "C ");


      if (inData == "DIS")
      {
        FastLED.setBrightness(0);
        FastLED.show();
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Disconnected!");
        delay(2000);
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Waiting for");
        lcd.setCursor(0, 1);
        lcd.print("connection...");
      }
      inData = "";
    }

    if (recieved == 'm')
    {
      inData.remove(inData.length() - 1, 1);

      for (int i = 0; i < NUM_LEDS / 2; i++)
      {
        leds[i] = CRGB(255, 255, 0);
      }

      for (int i = NUM_LEDS; i >= NUM_LEDS / 2; i--)
      {
        leds[i] =  CRGB(0, 0, 255);
      }
      FastLED.show();
      inData = "";

    }


    if (recieved == '$')
    {
      inData.remove(inData.length() - 1, 1);
      lcd.setCursor(10, 0);
      lcd.print(inData + char(37));
      inData = "";
    }

    if (recieved == '#')
    {
      inData.remove(inData.length() - 1, 2);
      lcd.setCursor(0, 1);
      lcd.print("CPU: " + inData + char(223) + "C ");
      inData = "";
    }

    if (recieved == '%')
    {
      inData.remove(inData.length() - 1, 1);
      lcd.setCursor(10, 1);
      lcd.print(inData + char(37));
      inData = "";
    }

    if (recieved == 't')
    {
      lcd.clear();
      inData.remove(inData.length() - 1, 1);
      lcd.setCursor(0, 0);
      lcd.print(inData);
      inData = "";
    }

    if (recieved == 'd')
    {
      inData.remove(inData.length() - 1, 1);
      lcd.setCursor(0, 1);
      lcd.print(inData);
      inData = "";
    }

    if (recieved == 'T')
    {
      inData.remove(inData.length() - 1, 1);
      data = inData.toInt();
      data = map(data, 30, 80, 130, 0);

      for (int i = 0; i < NUM_LEDS; i++ ) {
        leds[i] = CHSV(data, 255, 255);
      }
      FastLED.show();

      inData = "";
    }

    if (recieved == 'B')
    {
      inData.remove(inData.length() - 1, 1);
      data = inData.toInt();
      FastLED.setBrightness(data);
      inData = "";
    }

    if (recieved == 'W')
    {
      for (int i = 0; i < NUM_LEDS; i++ ) {
        leds[i] = CRGB(255, 128, 0);
        //leds[i] = CHSV(0, 0, 255);

      }
      FastLED.show();
      inData = "";
    }


    if (recieved == 'C')
    {
      inData.remove(inData.length() - 1, 1);
      int index = inData.indexOf(" ");
      String R = inData.substring(0, index);
      inData.remove(0, index + 1);
      index = inData.indexOf(" ");
      String G = inData.substring(0, index);
      inData.remove(0, index + 1);
      index = inData.indexOf(" ");
      String B = inData.substring(0, index);

      r = R.toInt();
      g = G.toInt();
      b = B.toInt();
      // data = map(data, 0, 359, 0, 239);
      for (int i = 0; i < NUM_LEDS; i++ ) {
        leds[i] = CRGB(r, g, b);
      }
      FastLED.show();

      inData = "";
    }

    if (recieved == 'A')
    {
      inData.remove(inData.length() - 1, 1);

      for (int i = 0; i < NUM_LEDS; i++) {


        int index = inData.indexOf(" ");
        String R = inData.substring(0, index);
        inData.remove(0, index + 1);
        index = inData.indexOf(" ");
        String G = inData.substring(0, index);
        inData.remove(0, index + 1);
        index = inData.indexOf(" ");
        String B = inData.substring(0, index);
        inData.remove(0, index + 1);

        r = R.toInt();
        g = G.toInt();
        b = B.toInt();

        if (i < NUM_LEDS / 2) {
          leds[i + NUM_LEDS / 2] = CRGB(r, g, b);
        }

        if (i >= NUM_LEDS / 2) {
          leds[NUM_LEDS - 1 - i] = CRGB(r, g, b);
        }

      }
      FastLED.show();
      inData = "";
    }


  }


}
