<%@ Page Title="dm网站操作指南" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OperationManual.aspx.cs" Inherits="OperationManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <div style="background-color: Silver; height: 500px; width: 850px; overflow: scroll;">
        <div id="area1" style="margin: 10px;">
            <span style="color: Red; font-weight: bold; font-size: 18px;">一、 登录</span><span style="margin-left:650px;"><a href="OperateManualWord.aspx">操作手册下载</a></span>
            <br />
            <span style="margin-left: 45px;">默认的用户名：<strong style="color: Red">Admin</strong> 密码：<strong
                style="color: red">Admin</strong> </span>
        </div>
        <div id="area2" style="margin: 10px;">
            <span style="color: Red; font-weight: bold; font-size: 18px;">二、 主界面</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">1. VIP资料管理</strong> (对公司VIP客户进行查询、修改查询结果、发送优惠券、发送短信)</span><br />
            <span style="margin-left: 45px;">具体的操作：</span><br />
            <span style="margin-left: 100px;">首先单击VIP资料管理—>选择查询条件 （什么条件都没有选择的情况下是默认查询所有的VIP信息，还可以关联调研问卷，就是针对某个进行过VIP调研问卷的客户进行查询)
                —>执行查询—>VIP查询结果界面:</span><br />
            <span style="margin-left: 100px;"><strong style="color: Green">1).上一步：</strong>返回上一步的操作</span><br />
            <span style="margin-left: 100px;"><strong style="color: Green">2).修改查询结果：</strong>就是对查询结果中的某个字段的信息进行修改，单击修改查询结果，
                弹出修改查询的对话框，可修改的字段：选择你想修改的字段值,比如你想修改某个VIP的积分， 就选择积分字段，将字段修改为表示，你想要修改成多少，下面有两个选项修改选中数据表示修改你在页面上选中的那些VIP,
                而修改全部数据表示修改所有的数据。</span><br />
            <span style="margin-left: 100px;"><strong style="color: Green">3).发送优惠券:</strong> 表示对VIP发送优惠券号，单击弹出申请以及发送优惠券的界面，选择优惠券主类型（就是有那些优惠券类型）,
                选择优惠券详细分类，选择有效截止日期（就是优惠券的有效时间），对选中用户发送（对页面上选中的用户进行发送）， 对所有的用户发送（对所有的用户发送）</span><br />
            <span style="margin-left: 100px;"><strong style="color: Green">4).发送短信及邮件：</strong>表示对VIP进行短信和邮件的发送，单击进入短信和邮件的发送界面
            </span>
            <br />
            <span style="margin-left: 116px;"><strong style="color: Green">①短信发送</strong> —>已保存短信（如果事先有保存过的短信，就选择，删除，表示删除以前保存的短信）
                —>添加属性字段（这个是用来在短信内容中填充内容的，例如短信内容：尊敬的[%姓名%]您好， 感谢您一直以来对本店的光顾，特发此短息表示感谢。其中的[%姓名%]就是通过添加属性字段来添加到短信内容中去的）
                —>短信内容简介（就是短信的标题，对短信进行一个简短的描述）—>短信内容（就是你要对VIP客户发送的真正的短信内容） —>发送通道（一般通道：表示一般的短信类型，广告通道：表示带有营销性质的短信内容）
                —>保存短信（表示对本条短信的内容进行保存，这样下一次就不用编写，就可以直接选择已保存短信进行发送） —>对所选用户发送短信（对前面页面中选中的用户进行短信的发送）—>对所有用户发送短息（表示对所有的用户进行短信的发送）。</span>
            <br />
            <span style="margin-left: 116px;"><strong style="color: Green">提示：如果你只是想简单地发送短信，你只需要在短信内容文本框里面填写你要发送的短息内容，
                然后选择哪个通道，点击对所选用户发送或者对所有用法发送即可。</strong> </span>
            <br />
            <span style="margin-left: 116px;"><strong style="color: Green">②邮件发送：</strong> 表示对前面选中的用户进行邮件的发送（邮件的发送只能一页一页地进行发送，
                而短信却是可以对所有的用户进行发送，不用一页一页地进行发送）读取模板(浏览…选择模板文件) —>加载模板文件（把模板文件加载进来，这样才能进行发送）—>发送邮件（把加载进来的模板文件发送给前面选择的用户）</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">2. 下载VIP资料</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">下载VIP资料—>下载数据内容—>资料内容—>VIP资料（表示下载VIP客户的信息（从ERP软件中））
                —>营业员资料（下载店铺的营业员的资料）—>门店资料（下载店铺的信息资料） —>VIP制卡（下载VIP卡的信息，就是我们在给VIP开卡的时候这个有用的） 提示：可以根据需求，进行资料的下载，不需要进行所有的资料进行下载
                （比如有时候，我们只想更新VIP客户的信息，这个时候我们就可以只选择VIP资料）</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">3. 数据字段设定</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">数据字段设定（就是为了在查询VIP资料的时候能够显示哪些字段的信息， 哪些字段可以作为查询的条件进行筛选，其中页面中加粗的红色字段表示我们在设置数据字段的时候，
                这个几个字段是必须要选中的）—>查询结果可见（表示查询VIP客户资料的时候，哪些字段， 我们可以在查询的结果中可以看见的）—>是否作为查询条件（表示哪些字段可以作为查询VIP客户信息的条件进行查询）
                提示：我们的设定只能一页一页进行保存，因为在进行页面的切换过程中，前面所有的项会丢失， 所有我们在某一页进行了选择之后，马上进行保存设定，然后再去切换到另一页</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">4.优惠券系统管理</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">优惠券系统管理（对申请过的优惠券（就是发送给客户的优惠券进行管理， 其中可以包括VIP客户已经兑换过的和没有兑换过的））</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">①优惠券查询</strong> —>查询条件—>发放店铺（就是优惠券是哪个店铺发放的，这个可以不填写，
                不填写就是差查询所有店铺发放的优惠券）—>使用状态（表示优惠券是否已经兑换， 至少要选择其中一个，否则查询不到数据）—>是否匹配VIP信息（是：表示就要匹配VIP信息，
                那可以匹配卡号或者手机号或者姓名，否：就是不匹配）—>发放日期区间（就是优惠券发放的日期区间段， 这个可以不用选择，不选择就是默认查询所有发放时间段得优惠券）—>到期日期区间（就是优惠券过期的日期时间段，
                这个也可以不用选择，不选择就表示插叙所有过期时间的优惠券）—>执行查询 —>查询结果(就会显示哪些VIP已经接受到我们发放的优惠券以及优惠券的兑换情况) —>详细信息（单击查询结果页面的第一列详细信息，就会显示优惠券的详细信息）
                —>发送优惠券编号（就是通过短信的形式，再一次对该手机用户进行短信的通知） —>兑换优惠券（就是对VIP客户接受到得优惠券进行兑换）</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">②类别管理</strong> —>优惠券生成（就是当优惠券数量不够的时候，我们可以输入想要生成的优惠券数量）
                —>新增类别（就是可以新增优惠券的类别，单击弹出新增优惠券类别的界面：优惠券主类型 —>优惠券详细类别（就是一个简短的介绍）—>使用场所（就是优惠券的可以兑换的地方）
                —>所有积分（就是该优惠券的类型需要多少积分）—>对应金额（就是该优惠券能够兑换多少的金额） —>注册新类型（就是保存该优惠券类型））</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">5. 短信模板编辑</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">短信模板编辑（就是有关开卡，销售短信，积分兑换（成功！）， 积分兑换（失败）注意这些短信模板的设定都是应用在pos机上进行短信的发送）
                开卡短信（就是通过pos机进行开卡成功之后，就会发送短信告诉VIP客户） —>可使用字段（就是有哪些字段可以出现在短信内容中， 例如：尊敬的顾客[姓名]您好,您已注册成为我公司会员,卡号为[卡号],初始密码为[密码],谢谢光临。
                其中[姓名]，[卡号]，[密码]就是通过可使用字段进行添加到短信内容中去的） —>保存并替换（就是将新编辑的短信模板内容保存下来）—>获取该类别已经存在的模板内容
                （就是获取已经保存过的该短信模板的内容）。其他类型的短信操作也是一样的。</span><br />
            <span style="margin-left: 45px;"><strong style="color: Green">6. 用户信息管理</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">用户信息管理（就是管理该dm网站系统账户的功能，有修改密码，删除用户， 添加用户的功能）—>修改密码（单击修改密码栏的图标，就可以修改密码，然后单击勾就是保存，
                红叉就是不保存）—>删除此账户（就是删除该系统账户）—>注册新账户（在用户名栏输入要添加的用户， 登录密码栏输入密码，然后单击添加就ok啦） </span>
            <br />
            <br />
            <span style="margin-left: 45px;"><strong style="color: Green">7. VIP信息注册</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">VIP信息注册（就是添加新的VIP信息），红色字体标明的就是必须要填写的字段 </span>
            <br />
            <span style="margin-left: 45px;"><strong style="color: Green">8. 销售分析 </strong></span>
            <br />
            <span style="margin-left: 45px;">销售分析（就是对VIP客户进行销售分析，查看VIP消费记录）—>设置查询条件 —>选择排序依据（就是按什么对查询结果进行排序）—>选择查询条件(就是查询符合什么条件的VIP消费记录，
                如果什么条件也不选择的话，就是选择所有的VIP消费记录)—>按条件查询—>消费记录查询结果 —>双击某一行弹出该VIP客户的详细消费记录 </span>
            <br />
            <span style="margin-left: 45px;"><strong style="color: Green">9. 自动短信管理</strong>
            </span>
            <br />
            <span style="margin-left: 45px;"><strong style="color: Green">1). 自动短信管理</strong> （这里的短息都是设定固定日期，系统自动发送的）—>自动短息设定
                —>发送时间类型 （<strong style="color: Green">①动态日期</strong> （如生日，开卡日期）<strong style="color: Green">②固定日期</strong>
                （就是设定一个固定的日期进行短息的发送<br />
                <strong style="color: Green">③节日</strong>（那就是设定在某个固定的节日里进行短信的发送））） —>发送日期（如果是选择项，就选择其中的一项，
                如果是要填写的就填写某个固定的日期进行短信的发送） —>发送日期调整(就是在设定的日期的基础上进行提前还延后进行短信的发送) —>模板概述(就是对该短信模板进行一个简单的说明)—>短信内容（编辑该短信模板的内容）
                —>发送目标筛选条件（这个是对短信内容的某个字段进行条件限定的， 比如，短信内容中含有[启用日期]，那么可以设定启用日期大于某个时间段而且小于某个时间段， 当然这里也可以不用填写）—>附属功能（就是在发送该短信的时候，是否附赠优惠券或者附赠多少积分）
                —>是否附赠优惠券（附赠：就是附赠某个类型的优惠券，可以选择已经有的优惠券类型,暂不附赠： 就表示不附赠优惠券信息）—>是否赠送积分（附赠：就是附赠积分，输入积分面值，暂不附赠：就是不附赠积分）
                —>生成自动短息模板（就是将该模板进行保存）。<strong style="color: Green"></span> <br /><span style="margin-left: 45px;">2). 自动短息管理</strong> 对已经保存了的自动短信进行管理（可以删除，就是删除以前已经存在的自动短息模板，
                使用状态，如果打钩就是表示启用，不打钩就表示不启用，最后保存所做的修改）</span>
            <br />
            <span style="margin-left: 45px;"><strong style="color: Green">10. 调研问卷管理</strong>
            </span>
            <br />
            <span style="margin-left: 45px;">调研问卷管理（就VIP客户进行调研问卷）启用状态（打钩表示启用，不打钩不启用） 转向测试（就是针对某个VIP客户进行调研问卷）—>输入VIP卡号（就是输入需要调研问卷的那个VIP卡号）
                新建调研问卷—>调研问卷简介—>内容描述(内容类型选择复选框时， 内容描述为性别，那么复选/单选组内容预编译输入：男,女，这样进行操作)—>单击添加一个问卷内容
                —>下一步—>就可以看到刚才添加过的调研问卷的内容，也可以点击删除按钮进行删除，或点击下一步进行保存</span>
            <br />
        </div>
    </div>
</asp:Content>
