import {Layout, Avatar, Menu, Dropdown, Breadcrumb} from 'ant-design-vue'

import {SettingOutlined, SearchOutlined,MenuFoldOutlined, MenuUnfoldOutlined} from '@ant-design/icons-vue';

export default {
    [Layout.Header.name]: Layout.Header,
    [Avatar.name]: Avatar,
    [Menu.name]: Menu,
    [Menu.Divider.name]: Menu.Divider,
    SettingOutlined,
    Dropdown,
    SearchOutlined,
    [Breadcrumb.name]: Breadcrumb,
    [Breadcrumb.Item.name]: Breadcrumb.Item,
    MenuFoldOutlined, MenuUnfoldOutlined
}
