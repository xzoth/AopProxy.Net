# AopProxy.NET
##### A simple .NET AOP component that make by dynamic proxy.

[![Build status](https://ci.appveyor.com/api/projects/status/ehqpox17oc2pai5v?svg=true)](https://ci.appveyor.com/project/xzoth/aopproxy-net)

[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu)

Nuget
https://www.nuget.org/packages/AopProxy.Net

------
## Requirements
.NET Framework 4.5

## Quick Start
1. Install nuget package  

```
Install-Package AopProxy.Net
```

2. Define an advice  

```
public class BeforeAdvice : IBeforeAdvice
{
    public void BeforeInvoke(InterceptorContext context)
    {
        Console.WriteLine("before enter method oh yeah");
    }
}
```

3. Define ur Logic Method you wanna to intercept  

```
public interface ILogic
{
    int Add(int a, int b);
}
public class LogicObject : ILogic
{
    public LogicObject() { }

    [JoinPoint]
    public int Add(int a, int b)
    {
        var result = a + b;
        Console.WriteLine("Result is: {0}", result);
    }
}
```

4. Configuration in ur App.config/Web.config  

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="AopProxy" type="AopProxy.AOP.Config.AopProxyConfigSectionHandler, AopProxy" />
  </configSections>
  <AopProxy>
    <Advisors>
      <Advisor PointCutType="AopProxy.AOP.Attribute.JoinPointAttribute, AopProxy" AdviseType="Demo.Advice.BeforeAdvice, Demo" />
    </Advisors>
  </AopProxy>
</configuration>
```

**That's it!**  

```
static void Main(string[] args)
{
    ILogic obj = AopProxyFactory.GetProxy<ILogic, LogicObject>();
    obj.Add(3, 3);

    Console.Read();
}
```

![image](https://github.com/xzoth/AopProxy.Net/blob/master/Screenshot/quickStart.png?raw=true)


## Concepts

### Advice

### JoinPoint

### PointCut

### Advisor


### Implement principle
#### Dymanic Proxy in .NET
#### Real Proxy Class


### AopProxy
### AopProxyFactory
### InterceptorContext
### JoinPointAttribute


## License
New BSD License

[![LICENSE](https://img.shields.io/badge/license-Anti%20996-blue.svg)](https://github.com/996icu/996.ICU/blob/master/LICENSE)

Copyright (c) 2017, All rights reserved.  

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:  

- Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.  
- Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.  
- Neither the name of vigo nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.  

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
