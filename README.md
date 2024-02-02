# SpaceRabbit
## 描述
这是基于DDD微服务架构的个人博客系统后端。

## 技术栈

* Web框架采用.Net Core WebApi
* ORM采用EF Core
* 数据库使用MySQL，除了`T_Configs`主键均采用GUID
* 支持Redis分布式缓存和Memcached内存式缓存
* 日志使用Serilog
* 服务间消息队列使用RabbitMQ
* 进程内部消息传递使用MediaR
* 规则校验使用FluentValidation
* 使用JWT校验

## 接口
接口尽量遵循RESTful风格靠拢HTTP语义。

接口文档尚未整理。


## 配置

### 配置数据库

在启动项目前，需在环境变量中设置数据库连接字符串，变量名为`DefaultDB:ConnStr`，其值可以按如下格式填写。
```
server=127.0.0.1;port=3306;user=db_spacerabbit;database=db_spacerabbit;password=db_spacerabbit;
```

然后再数据库中创建`T_Configs`表来进行其他配置。当然你也可以将这些配置填写在环境变量中，但强烈建议放入数据库中。
该表包含三个属性：
* `Id(int)`
* `Name(text)`
* `Value(text)`

其中`Name`为配置名称，`Value`为内容支持Json等文本格式，但强烈建议统一使用Json。

### 配置Redis

在`T_Configs`中插入`Name`为`Redis`的字段，其`Value`如下

```json
{
    "ConnStr":"127.0.0.1"
}
```

### 配置RabbitMQ

请确保RabbitMQ正确部署且创建具有相应权限的用户。
该部分`Name`为`RabbitMQ`，其`Value`部分可按照下面格式填写

```json
{
    "HostName":"127.0.0.1",
    "ExchangeName":"SpaceRabbit_event_bus",
    "UserName":"SpaceRabbit",
    "Password":"spacerabbitpwd"
}
```

### 配置跨域
跨域配置的`Name`为`Cors`，其`Value`可如下格式
```json
{
    "Origins":["http://localhost:3000","http://localhost:3001"]
}
```

### 配置JWT
`Name`为`JWT，其`Value`格式如下
```json
{
    "Issuer":"myIssuer",
    "Audience":"myAudience",
    "Key":"pleasesetyourkey1234567890",
    "ExpireSeconds":114514000
}
```

需要注意，这里`Key`部分长度需大于等于16

### 配置Google验证码
其`Name`为`Recaptcha`，`Value`格式如下

```json
{
    "SiteKey":"xxxxxxxxxx",
    "SecretKey":"xxxxxxxxxxxx",
    "Version":"v3",
    "UseRecaptchaNet":false,
    "ScoreThreshold":0.5,
    "Domain":"www.recaptcha.net"
}
```

配置前请到官网去申请。

## 其他
TO DO ....